using Core.Contracts.Auth.Login;
using Core.Contracts.Auth.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Service.ServiceInterfaces;

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

            if (result is not null)
                return Ok(result);

            return BadRequest("Email or Pass are invalid!");       
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Rigester([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var id = await _authService.RegisterAsync(request , cancellationToken);

            if (id is not null)
                return Created();
            return BadRequest("Pleas enter valid information !..");
        }
    }
}
