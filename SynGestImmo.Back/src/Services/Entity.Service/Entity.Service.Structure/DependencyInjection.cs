

using Entity.Service.Application.Common;
using Entity.Service.Domain.Repositories.EntityRepositories;
using Entity.Service.Domain.Repositories.PersonRepositories;
using Entity.Service.Infrastructure.Handlers;
using Entity.Service.Structure.Data;
using Entity.Service.Structure.Data.Repositories;
using Entity.Service.Structure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Entity.Service.Structure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
          
            services.AddScoped<SqlExceptionsHandler>();

            services.AddScoped<IUowCreateEntity, UowCreatingEntity>();

            services.AddScoped<IEntityService, EntityService>();

            services.AddScoped<IDapperConnection, DapperConnection>();

            // pour les identity
            services.AddScoped<EntitySgiRepository>();
            services.AddScoped<IEntityCommandRepository>(sp => sp.GetRequiredService<EntitySgiRepository>());
            services.AddScoped<IEntityQueryRepository>(sp => sp.GetRequiredService<EntitySgiRepository>());

            // pour les person
            services.AddScoped<PersonRepository>();
            services.AddScoped<IPersonCommandRepository>(sp => sp.GetRequiredService<PersonRepository>());
            services.AddScoped<IPersonQueryRepository>(sp => sp.GetRequiredService<PersonRepository>());





            return services;
        }
    }
}
