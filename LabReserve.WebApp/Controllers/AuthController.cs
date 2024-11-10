using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;

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

        [HttpPost]
        [AllowAnonymous]
        [Route("{controller}/sing-in")]
        public async Task<IActionResult> SignIn([FromBody] AuthRequestDto request)
        {
            try
            {
                var result = await service.SignIn(request.Email, request.Password) ?? throw new Exception("User not found");
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

                return Json(new AuthResponseDto(result.Email, result.UserType, result.Name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{controller}/sign-out")]
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("{controller}/user")]
        public async Task<IActionResult> UserAuth()
        {
            var user = HttpContext.User;
            return Json(user);
        }
    }
}
