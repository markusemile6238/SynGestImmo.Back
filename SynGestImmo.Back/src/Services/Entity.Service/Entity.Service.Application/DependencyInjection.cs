using Entity.Service.Application.Features.IdentityFeature;
using Entity.Service.Application.Handler;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Reflection;

namespace Entity.Service.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddRefitClient<IIdentityApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://localhost:7001");
                })
                .AddHttpMessageHandler<TokenPropagationHandler>();

            return services;
        }
    }
}
