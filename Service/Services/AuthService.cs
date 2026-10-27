using Core.Contracts.Auth.Login;
using Core.Contracts.Auth.RefreshToken;
using Core.Contracts.Auth.Register;
using Core.Interfaces.Auth;
using Core.Models;
using Infrastructure.Data;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace Service.Services
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
        private readonly ILogger _logger = logger;

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // check user 
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return null!;

            // validate pass 
            var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if(!isValid)
                return null!;

            // generate Jwt token
             var result =  _jwtProvider.GenerateJwtToken(user);

            // generate refresh token 
            var refreshToken 
                        = await _refreshTokenServise.AssignRefreshTokenAsync(user , cancellationToken);

            if (refreshToken is null)
                return null!;

            // return token
            var objToReturn = new LoginResponse(
                user.Id, user.Email!, request.Password, 
                result.Token, result.Expiration,
                refreshToken.Value.token ,refreshToken.Value.expirationOn);
            
            return objToReturn;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(user , request.Password);

            if (result.Succeeded) 
            {
                var objToReturn = new RegisterResponse (user.Id);
                return objToReturn;
            }
            else
            {
                _logger.LogError(
                 string.Join(", ",
                        result.Errors.Select(x => x.Description)));
            }
            return null!;
        }

        public async Task<RefreshTokenResponse> GetRefreshTekenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // validate jwt token   
            var userId = _jwtProvider.ValidateJwtToken( request.OldJwtToken);
                                                                     
            if (userId is null)
                return null!;

            // validate refresh token 
            var applicationUser =
                        await _refreshTokenServise.ValidateRefreshTokenAsync( userId,
                                                                            request.OldRefreshToken,
                                                                                  cancellationToken);

            if (applicationUser is null)
                return null!;

            // revoke old refresh token 
            var isRevoved = await _refreshTokenServise.RevokeRefreshTokenAsync(userId,
                                                                             request.OldRefreshToken,
                                                                                   cancellationToken);

            if (!isRevoved)
                return null!;

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

            return objToReturn;
        }

        public async Task<bool> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // validate jwt token   
            var userId = _jwtProvider.ValidateJwtToken(request.OldJwtToken);

            if (userId is null)
                return false;

            return await _refreshTokenServise.RevokeRefreshTokenAsync(userId,
                                                                request.OldRefreshToken,
                                                                      cancellationToken);
        }
    }
}
