namespace SurveyBasket.WebApi.Controllers
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
               : result.ToProblem();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok()
               : result.ToProblem();
        }
        [HttpPut]
        [Route("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ResendConfirmationEmailAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok()
               : result.ToProblem();
        }
        [HttpPut]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok()
               : result.ToProblem();
        }
        [HttpPut]
        [Route("get-refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetRefreshTokenAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value)
               : result.ToProblem();
        }        
        [HttpPut]
        [Route("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenAsync(request , cancellationToken);

            return result.IsSuccess ?
                Ok("Token Revoked Successfuly.")
               : result.ToProblem();
        }    
    }
}
