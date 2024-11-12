using FluentValidation.AspNetCore;
using FluentValidation;
using LabReserve.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using LabReserve.Application.Validators;
using LabReserve.Application.Services;

namespace LabReserve.Application;

public static class ServiceExtensions
{
    public static void ConfigureServicesApp(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddValidatorsFromAssemblyContaining<AuthRequestValidator>();
        services.AddFluentValidationAutoValidation();
    }
}