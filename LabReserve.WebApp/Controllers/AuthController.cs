using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LabReserve.WebApp.Domain.Abstractions;

namespace LabReserve.WebApp.Controllers
{
    public class AuthController(IUserService service) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("access-denied")]
        public IActionResult AccessDenied() 
        {
            return View();
        }

        [Route("sign-in")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAuth()
        {
            try
            {
                var result = await service.SignIn("", "");
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, result.Name),
                    new(ClaimTypes.Email, result.Email),
                    new(ClaimTypes.Sid, result.Id.ToString()),
                    new(ClaimTypes.Role, result.UserType.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("sign-out")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { success = true });
        }
    }
}
