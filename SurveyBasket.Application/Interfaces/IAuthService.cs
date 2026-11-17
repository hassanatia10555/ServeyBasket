namespace SurveyBasket.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellation);
        Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellation);
        Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellation);
        Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken cancellation);
        Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request, CancellationToken cancellation);
    }
}
