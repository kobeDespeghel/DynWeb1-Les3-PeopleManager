using Microsoft.AspNetCore.Mvc;
using PeopleManager.Api.Services;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(
        IdentityService identityService,
        AuthenticationManager authenticationManager) : ControllerBase
    {
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var result = await identityService.SignIn(request);
            if(result.IsSuccess || result.Data is null)
            {
                var authenticationResult = new AuthenticationResult();
                authenticationResult.Messages = result.Messages;
                return Ok(authenticationResult);
            }

            var token = authenticationManager.GenerateJwtToken(result.Data);
            return Ok(new AuthenticationResult
            {
                Token = token,
                Messages = result.Messages
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await identityService.Register(request);
            if (result.IsSuccess || result.Data is null)
            {
                var authenticationResult = new AuthenticationResult();
                authenticationResult.Messages = result.Messages;
                return Ok(authenticationResult);
            }

            var token = authenticationManager.GenerateJwtToken(result.Data);
            return Ok(new AuthenticationResult
            {
                Token = token,
                Messages = result.Messages
            });
        }
    }
}
