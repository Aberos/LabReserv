using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Enums;

namespace LabReserve.WebApp.Infrastructure
{
    public class AuthUser(IHttpContextAccessor httpContextAccessor) : IAuthUser
    {
        public long? Id => httpContextAccessor?.HttpContext?.GetAuthUser()?.Id;
        public UserType UserType => httpContextAccessor?.HttpContext?.GetAuthUser()?.UserType ?? UserType.Professor;
        public string Name => httpContextAccessor?.HttpContext?.GetAuthUser()?.Name!;
        public string Email => httpContextAccessor?.HttpContext?.GetAuthUser()?.Email!;
    }
}
