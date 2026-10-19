using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LabReserve.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);
    }
}
