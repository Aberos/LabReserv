using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Entities;
using LabReserve.Persistence.Abstractions;
using LabReserve.Persistence.Database;
using LabReserve.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabReserve.Persistence;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructureApp(this IServiceCollection services,
          IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped<IDbSession, DbSession>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
    }
}