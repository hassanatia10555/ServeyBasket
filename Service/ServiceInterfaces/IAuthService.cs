using Core.Contracts.Auth.Login;
using Core.Contracts.Auth.Register;

namespace Service.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request , CancellationToken cancellationToken);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellation);
    }
}
