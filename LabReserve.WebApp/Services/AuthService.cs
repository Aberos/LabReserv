using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using LabReserve.Domain.Enums;
using LabReserve.WebApp.Extensions;

namespace LabReserve.WebApp.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public long? Id => httpContextAccessor?.HttpContext?.GetAuthUser()?.Id;
    public UserType UserType => httpContextAccessor?.HttpContext?.GetAuthUser()?.UserType ?? UserType.Professor;
    public string Name => httpContextAccessor?.HttpContext?.GetAuthUser()?.Name!;
    public string Email => httpContextAccessor?.HttpContext?.GetAuthUser()?.Email!;

    public async Task SetUser(UserAuthDto userAuthDto)
    {
        if (httpContextAccessor?.HttpContext is null)
            throw new Exception("HttpContext not found");

        await httpContextAccessor.HttpContext.SetAuthUser(userAuthDto)!;
    }

    public async Task RemoveUser()
    {
        if (httpContextAccessor?.HttpContext is null)
            throw new Exception("HttpContext not found");

        await httpContextAccessor?.HttpContext?.RemoveAuthUser()!;
    }
}