using Core.Contracts.Auth.Login;
using Core.Contracts.Auth.RefreshToken;
using Core.Contracts.Auth.Register;

namespace Core.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request , CancellationToken cancellationToken);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellation);
        Task<RefreshTokenResponse> GetRefreshTekenAsync(RefreshTokenRequest request, CancellationToken cancellation);
        Task<bool> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellation);
    }
}
