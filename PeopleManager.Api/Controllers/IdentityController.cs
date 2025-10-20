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
            var serviceResult = await identityService.SignIn(request);
            var authenticationResult = new AuthenticationResult();


            if (!serviceResult.IsSuccess || serviceResult.Data is null)
            {
                authenticationResult.Messages = serviceResult.Messages;
                return Ok(authenticationResult);
            }

            var token = authenticationManager.GenerateJwtToken(serviceResult.Data);
            authenticationResult.Token = token;
            return Ok(authenticationResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var serviceResult = await identityService.Register(request);
            var authenticationResult = new AuthenticationResult();

            if (!serviceResult.IsSuccess || serviceResult.Data is null)
            {
                authenticationResult.Messages = serviceResult.Messages;
                return Ok(authenticationResult);
            }

            var token = authenticationManager.GenerateJwtToken(serviceResult.Data);
            authenticationResult.Token = token;
            return Ok(authenticationResult);
        }
    }
}
