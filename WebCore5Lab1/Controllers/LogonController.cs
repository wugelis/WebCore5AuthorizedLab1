using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCore5Lab1.Models;

namespace WebCore5Lab1.Controllers
{
    public class LogonController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogonController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateModel account)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal principal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, account.Username),
                        new Claim(ClaimTypes.Role, "Admin")
                    },
                    "Custom Scheme"
                ));

                await _httpContextAccessor.HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
