using LabReserve.Domain.Dto;
using LabReserve.Domain.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace LabReserve.WebApp.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task SetAuthUser(this HttpContext context, UserAuthDto user)
        {
            if (context is null)
                throw new Exception("HttpContext not found.");

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Sid, user.Id.ToString()),
                new(ClaimTypes.Role, user.UserType.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { };

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public static async Task RemoveAuthUser(this HttpContext context)
        {
            if (context is null)
                throw new Exception("HttpContext not found.");

            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public static UserAuthDto GetAuthUser(this HttpContext context)
        {
            if (context?.User is null)
                return null!;

            var sid = context.User.FindFirstValue(ClaimTypes.Sid);
            var id = !string.IsNullOrWhiteSpace(sid) && long.TryParse(sid, out var userId) ? userId : 0;
            var name = context.User.FindFirstValue(ClaimTypes.Name)!;
            var email = context.User.FindFirstValue(ClaimTypes.Email)!;
            var role = context.User.FindFirstValue(ClaimTypes.Role)!;
            var userType = Enum.TryParse(typeof(UserType), role, out var parseUserType) ? (UserType)parseUserType : UserType.Professor;

            return new UserAuthDto { Id = id, Name = name, Email = email, UserType = userType };
        }
    }
}
