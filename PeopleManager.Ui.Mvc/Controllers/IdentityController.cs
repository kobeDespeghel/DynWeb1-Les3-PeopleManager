using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Sdk;
using PeopleManager.Ui.Mvc.Extensions;
using PeopleManager.Ui.Mvc.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using Vives.Security;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class IdentityController(IdentitySdk identitySdk, ITokenStore tokenStore) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> SignIn(string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }
            ViewBag.ReturnUrl = returnUrl;

            await InternalSignOut();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInRequest = new SignInRequest
            {
                Email = signInModel.Email,
                Password = signInModel.Password
            };

            var signInResult = await identitySdk.SignIn(signInRequest);



            if (!signInResult.IsSuccess)
            {
                ModelState.AddServiceMessages(signInResult.Messages);
                return View();
            }

            await InternalSignIn(signInResult.Token);

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult Register(string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }
            ViewBag.ReturnUrl = returnUrl;

            var registerRequest = new RegisterRequest
            {
                Email = registerModel.Email,
                Password = registerModel.Password
            };



            var registerResult = await identitySdk.Register(registerRequest);
            if (!registerResult.IsSuccess)
            {
                ModelState.AddServiceMessages(registerResult.Messages);
                return View();
            }

            await InternalSignIn(registerResult.Token);

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await InternalSignOut();

            return RedirectToAction("Index", "Home");
        }

        private async Task InternalSignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            tokenStore.Clear();
        }


        private async Task InternalSignIn(string? token)
        {
            if (String.IsNullOrWhiteSpace(token)) return;

            tokenStore.SetToken(token);

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token.Trim());
            var claimsList = jwtToken.Claims.ToList();

            var email = jwtToken.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")
                ?.Value;

            if(!String.IsNullOrWhiteSpace(email))
            {
                claimsList.Add(new Claim(ClaimTypes.Name, email));
            }

            var identity = new ClaimsIdentity(claimsList, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(claimsPrincipal);
        }
    }
}
