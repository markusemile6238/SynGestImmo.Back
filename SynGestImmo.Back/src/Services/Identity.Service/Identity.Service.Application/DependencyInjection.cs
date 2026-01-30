using Identity.Service.Application.Common;
using Identity.Service.Application.Features.UserFeature.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Identity.Service.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Enregistrer MediatR avec tous les handlers de l'assembly
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IUserReferenceService, UserReferenceService>();

           

            return services;
        }
    }
}
