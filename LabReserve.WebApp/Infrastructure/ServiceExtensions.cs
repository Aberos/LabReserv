using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Infrastructure.Abstractions;
using LabReserve.WebApp.Infrastructure.Database;
using LabReserve.WebApp.Infrastructure.Repositories;

namespace LabReserve.WebApp.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructureApp(this IServiceCollection services, 
          IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped<IDbSession, DbSession>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}