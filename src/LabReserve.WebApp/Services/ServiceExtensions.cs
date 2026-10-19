using LabReserve.Application;
using LabReserve.Application.Behaviors;
using LabReserve.Domain.Abstractions;

namespace LabReserve.WebApp.Services;

public static class ServiceExtensions
{
    public static void ConfigureServicesWebApp(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Assemblies = [typeof(AssemblyReference)];
            options.PipelineBehaviors = [typeof(ValidationBehavior<,>), typeof(TransactionBehavior<,>)];
        });
    }
}
