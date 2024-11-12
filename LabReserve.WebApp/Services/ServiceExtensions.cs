using LabReserve.Domain.Abstractions;

namespace LabReserve.WebApp.Services;

public static class ServiceExtensions
{
    public static void ConfigureServicesWebApp(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
    }
}