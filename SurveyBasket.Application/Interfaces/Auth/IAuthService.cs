namespace SurveyBasket.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request , CancellationToken cancellationToken);
        Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellation);
        Task<Result<RefreshTokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellation);
        Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellation);
    }
}
