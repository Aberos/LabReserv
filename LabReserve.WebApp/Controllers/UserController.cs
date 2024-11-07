using System.Security.Claims;
using LabReserve.WebApp.Domain.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers;

[Authorize("admin")]
public class UserController(IUserService service) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Auth()
    {
        return View();
    }

    [HttpPost("auth")]
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
            var authProperties = new AuthenticationProperties{ };

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
    
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { success = true });
    }
}