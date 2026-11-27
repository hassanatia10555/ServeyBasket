using Hangfire;
using SurveyBasket.Application.Contracts.TemplateModels;
using SurveyBasket.Application.Interfaces.NotificationService.Abstractions;
using SurveyBasket.Domain.Constants;

namespace SurveyBasket.Application.Services
{
    public class AuthService(
          IJwtProvider jwtProvider,
          IRefreshTokenService refreshTokenService,
          IMapper mapper,
          ILogger<AuthService> logger,
          IIdentityService identityService,
          IHttpContextAccessor httpContextAccessor,
          INotificationSenderFactory notificationSender,
          IMessageBodyBuilderFactory confirmationEmailMessage) : IAuthService
    {
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IRefreshTokenService _refreshTokenServise = refreshTokenService;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly IIdentityService _identityService = identityService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly INotificationSenderFactory _notificationSenderFactory = notificationSender;
        private readonly IMessageBodyBuilderFactory _confirmationEmailMessageFactory = confirmationEmailMessage;

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // check user 
            if (await _identityService.FindByEmailAsync(request.Email) is not ApplicationUser user)
                return Result.Failure<LoginResponse>(UserErrors.IvalidCredentials);

            // validate pass 
            var result = await _identityService.PasswordSignInAsync(user, request.Password, false, false);

            // generate Jwt token
            if (result.Succeeded)
            {
                var jwtToken = _jwtProvider.GenerateJwtToken(user);

                // generate refresh token 
                var refreshToken
                            = await _refreshTokenServise.AssignRefreshTokenAsync(user, cancellationToken);

                if (refreshToken is null)
                    return Result.Failure<LoginResponse>(UserErrors.IvalidCredentials);

                // return token
                var objToReturn = new LoginResponse(
                    user.Id, user.Email!, request.Password,
                    jwtToken.Token, jwtToken.Expiration,
                    refreshToken.Value.token, refreshToken.Value.expirationOn);

                return Result.Success(objToReturn);
            }
            return Result.Failure<LoginResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.UserNotFound);
        }
        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (await _identityService.FindByEmailAsync(request.Email) is not null)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _identityService.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // generate email confirmation token

                var code = await _identityService.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                _logger.LogInformation(
                    "User registered successfully. User ID: {UserId} \n, Email: {Email}\n Code: {Code}",
                    user.Id, user.Email, code);

                await SendConfirmationEmailAsync(user);
                 
                return Result.Success();
            }

            return Result.Failure(UserErrors.IvalidCredentials);
        }
        public async Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            // check email exists 
            if (await _identityService.FindByEmailAsync(request.Email) is not ApplicationUser user)
                return Result.Success(); //  we return success even if the user is not found.

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            // generate email confirmation token
            var code = await _identityService.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation(
                "Confirmation Email Accepted successfully. User ID: {UserId} \n, Email: {Email}\n Code: {Code}",
                user.Id, user.Email, code);

            // send email
            await SendConfirmationEmailAsync(user);

            return Result.Success();
        }
        public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            if (await _identityService.FindByIdAsync(request.UserId) is not ApplicationUser user)
                return Result.Failure(UserErrors.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
            }
            catch (FormatException ex)
            {
                _logger.LogError(ex, "Error decoding confirmation code for user {UserId}", request.UserId);
                return Result.Failure(UserErrors.InvalidCode);
            }

            var result = await _identityService.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
                return Result.Success();

            return Result.Failure(UserErrors.InvalidCode);
        }
        public async Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // validate jwt token   
            var userId = _jwtProvider.ValidateJwtToken(request.OldJwtToken);

            if (userId is null)
                return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidJwtToken);

            // validate refresh token 
            var applicationUser =
                        await _refreshTokenServise.ValidateRefreshTokenAsync(userId,
                                                                            request.OldRefreshToken,
                                                                                  cancellationToken);

            if (applicationUser is null)
                return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);

            // revoke old refresh token 
            var isRevoked = await _refreshTokenServise.RevokeRefreshTokenAsync(userId,
                                                                             request.OldRefreshToken,
                                                                                   cancellationToken);

            if (!isRevoked)
                return Result.Failure<RefreshTokenResponse>(UserErrors.RevokingToken);

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
                return Result.Failure<RefreshTokenResponse>(UserErrors.InvalidRefreshToken);

            var isRevoked = await _refreshTokenServise.RevokeRefreshTokenAsync(userId!,
                                                                request.OldRefreshToken,
                                                                      cancellationToken);
            if (!isRevoked)
                return Result.Failure(UserErrors.RevokingToken);

            return Result.Success();
        } 
        private async Task SendConfirmationEmailAsync(ApplicationUser user)
        {
            var messageBuilder = 
                _confirmationEmailMessageFactory.Create<ConfirmationEmailTemplateModel>(
    NotificationChannel.Email, NotificationMessageType.ConfirmationEmail);

            var message = messageBuilder.Build(
                new ConfirmationEmailTemplateModel(
                    $"{user.FirstName} {user.LastName}",
                    "confirmation-link",
                    "Survey Basket",
                    2026));

            var notificationSender =
                _notificationSenderFactory.Create(NotificationChannel.Email);

            //var sentResult = await notificationSender.SendAsync(
            //    user.Email!,
            //    message
            //    , "Conifirmation Email");

            BackgroundJob.Enqueue(() => notificationSender.SendAsync(user.Email!,
                message
                , "✅ Conifirmation Email"));

            await Task.CompletedTask;
        }
    }
}