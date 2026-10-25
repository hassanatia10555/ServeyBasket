using Core.Authentication;
using Core.Contracts.Auth.Login;
using Core.Contracts.Auth.Register;
using Core.Models;
using Infrastructure.Data;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager 
                            ,IJwtProvider jwtProvider
                            ,IMapper mapper
                            ,ApplicationDbContext dbContext
                            ,ILogger<AuthService> logger) : IAuthService
    {
        private  UserManager<ApplicationUser> _userManager { get; } = userManager;
        private IJwtProvider _jwtProvider  = jwtProvider;
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ILogger _logger = logger;

        public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            // check user 
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return null!;

            // validate pass 
            var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if(!isValid)
                return null!;

            // generate token
             var result =  _jwtProvider.GenerateToken(user);

            // return token
            var objToReturn = new LoginResponse(
                user.Email!, request.Password, 
                result.Token, result.Expiration,
                null! );
            
            return objToReturn;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellation)
        {
            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(user , request.Password);

            if (result.Succeeded) 
            {
                await _dbContext.SaveChangesAsync();

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
    }
}
