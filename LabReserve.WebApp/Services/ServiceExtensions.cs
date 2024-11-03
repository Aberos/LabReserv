using LabReserve.WebApp.Domain.Abstractions;

namespace LabReserve.WebApp.Services;

public static class ServiceExtensions
{
    public static void ConfigureServicesApp(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
    }
}