using SurveyBasket.Application.Errors;

namespace SurveyBasket.Application.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager 
                            ,IJwtProvider jwtProvider
                            ,IRefreshTokenService refreshTokenService
                            ,IMapper mapper
                            ,ILogger<AuthService> logger) : IAuthService
    {
        private  UserManager<ApplicationUser> _userManager { get; } = userManager;
        private IJwtProvider _jwtProvider  = jwtProvider;
        private readonly IRefreshTokenService _refreshTokenServise = refreshTokenService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // check user 
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return Result.Failure<LoginResponse>(UserErrors.UserNotFound);

            // validate pass 
            var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if(!isValid)
                return Result.Failure<LoginResponse>(UserErrors.IvalidCredentials);

            // generate Jwt token
             var result =  _jwtProvider.GenerateJwtToken(user);

            // generate refresh token 
            var refreshToken 
                        = await _refreshTokenServise.AssignRefreshTokenAsync(user , cancellationToken);

            if (refreshToken is null)
                return Result.Failure<LoginResponse>(UserErrors.IvalidCredentials);

            // return token
            var objToReturn = new LoginResponse(
                user.Id, user.Email!, request.Password, 
                result.Token, result.Expiration,
                refreshToken.Value.token ,refreshToken.Value.expirationOn);
            
            return Result.Success(objToReturn);
        }

        public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(user , request.Password);

            if (result.Succeeded) 
            {
                var objToReturn = new RegisterResponse (user.Id);
                return Result.Success(objToReturn);
            }
            else
            {
                _logger.LogError(
                 string.Join(", ",
                        result.Errors.Select(x => x.Description)));

                return Result.Failure<RegisterResponse>(UserErrors.IvalidCredentials);
            }
        }

        public async Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // validate jwt token   
            var userId = _jwtProvider.ValidateJwtToken( request.OldJwtToken);

            if (userId is null)
                Result.Failure<RefreshTokenResponse>(UserErrors.InvalidJwtToken);

            // validate refresh token 
            var applicationUser =
                        await _refreshTokenServise.ValidateRefreshTokenAsync( userId!,
                                                                            request.OldRefreshToken,
                                                                                  cancellationToken);

            if (applicationUser is null)
                Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);

            // revoke old refresh token 
            var isRevoved = await _refreshTokenServise.RevokeRefreshTokenAsync(userId!,
                                                                             request.OldRefreshToken,
                                                                                   cancellationToken);

            if (!isRevoved)
                Result.Failure<RefreshTokenResponse>(UserErrors.RevokingToken);

            // create new tokens 
            var newJwtToken = _jwtProvider.GenerateJwtToken(applicationUser);

            var newRefreshToken =
                                 await _refreshTokenServise.AssignRefreshTokenAsync(applicationUser,
                                                                                          cancellationToken);

            // return tokens
            var objToReturn = new RefreshTokenResponse
            (
                 newRefreshToken!.Value.token,
                 newRefreshToken.Value.expirationOn,
                newJwtToken.Token,
                newJwtToken.Expiration
            );

            return Result.Success(objToReturn);
        }

        public async Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // validate jwt token   
            var userId = _jwtProvider.ValidateJwtToken(request.OldJwtToken);

            if (userId is null)
                Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);

            var isRevoked = await _refreshTokenServise.RevokeRefreshTokenAsync(userId!,
                                                                request.OldRefreshToken,
                                                                      cancellationToken);
            if (!isRevoked)
                return Result.Failure(UserErrors.RevokingToken);
            
            return Result.Success();
        }

    }
}
