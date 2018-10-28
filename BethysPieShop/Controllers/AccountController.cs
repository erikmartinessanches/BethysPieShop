using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethysPieShop.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BethysPieShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //SignInManager and UserManager will be injected automatically by DI
        //whereever I need them... no extra services need to be added.

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            ViewBag.CurrentPage = "Login";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            //Use the _userManager to see if we have a user with the specified user name.
            var user = await
                _userManager.FindByNameAsync(loginViewModel.UserName);

            //Try to sign in the user if we found a user in the step above.
            if (user != null)
            {
                var result = await
                    _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded) //Redirect to index action on the home controller if ok.
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //If not, show a generic error. We add a custom error to the model state, 
            //which will be shown in validation summary in the view.
            ModelState.AddModelError("", "User name/password not found");
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            ViewBag.CurrentPage = "Register";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //Manually craete an IdenityUser.
                var user = new IdentityUser() { UserName = loginViewModel.UserName };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);
                await _signInManager.SignInAsync(user, isPersistent: false); //Signs in the user directly after registration.
                if (result.Succeeded) //if all ok
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginViewModel); //if not.
        }

        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




        //[Route("signin")]

        //public IActionResult SignIn() => View();



        //[Route("signin/{provider}")]

        //public IActionResult SignIn(string provider, string returnUrl = null) =>

        //    Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);



        //[Route("signout")]

        //public async Task<IActionResult> SignOut()

        //{

        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //    return RedirectToAction("Index", "Home");

        //}
    }
}