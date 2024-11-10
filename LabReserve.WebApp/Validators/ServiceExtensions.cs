using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace LabReserve.WebApp.Validators
{
    public static class ServiceExtensions
    {
        public static void ConfigureValidatorsApp(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AuthRequestValidator>();
            services.AddFluentValidationAutoValidation();
        }
    }
}
