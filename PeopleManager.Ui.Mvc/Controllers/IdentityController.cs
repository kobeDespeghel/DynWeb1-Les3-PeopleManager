using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> SignIn(string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = "/";
            }
            ViewBag.ReturnUrl = returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            await _signInManager.SignOutAsync();

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

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username/Password combination is incorrect.");
                return View();
            }

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

            var identityUser = new IdentityUser(registerModel.Email)
            {
                Email = registerModel.Email
            };

            var result = await _userManager.CreateAsync(identityUser, registerModel.Password);
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
                return View();
            }

            await _signInManager.SignInAsync(identityUser, false);

            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
