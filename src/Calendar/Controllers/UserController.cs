using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Calendar.Services;
using Calendar.Models.SignOnViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Calendar.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Signing")]

    public class UserController : Controller
    {
        private readonly IAuthenticationService _authService;
        public UserController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        //
        // GET: /User/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //
        // POST: /User/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignOnViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _authService.Login(model.Username, model.Password);
                    if (null != user)
                    {
                        var userClaims = new List<Claim>
                    {
                        new Claim("displayName", user.DisplayName),
                        new Claim("username", user.Username)
                    };
                        if (user.IsAdmin)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, "Admins"));
                        }
                        var principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, _authService.GetType().Name));
                        await HttpContext.Authentication.SignInAsync(
                            "CalendarApp",
                            principal);
                        if (returnUrl == null)
                            return RedirectToAction("Calendar", "Events");
                        else
                            return Redirect(returnUrl);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        /*[Authorize(Roles = UserRoles.Everyone)]*/
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("CalendarApp");
            return RedirectToAction("Calendar", "Events");
        }
    }
}