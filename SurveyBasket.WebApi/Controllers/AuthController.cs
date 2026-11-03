using SurveyBasket.Application.Abstractions;
using System.Reflection.Metadata.Ecma335;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public IAuthService _authService = authService;

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody]LoginRequest request , CancellationToken cancellationToken) 
        {
            var result = await _authService.LoginAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value)
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Rigester([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value)
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }
        

        [HttpPut]
        [Route("get-refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetRefreshTokenAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value)
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }
        

        [HttpPut]
        [Route("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok("Token Revoked Successfuly.")
               : result.ToProblem(StatusCodes.Status400BadRequest);
        }    
    }
}
