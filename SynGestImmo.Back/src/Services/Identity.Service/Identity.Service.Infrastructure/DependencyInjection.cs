using Identity.Service.Application.Common;
using Identity.Service.Domain.Repositories.RoleRepositories;
using Identity.Service.Domain.Repositories.UserRolesRepositories;
using Identity.Service.Infrastructure.Data;
using Identity.Service.Infrastructure.Data.Repositories;
using Identity.Service.Infrastructure.Data.Repositories.UserRepositories;
using Identity.Service.Infrastructure.Handlers;
using Identity.Service.Infrastructure.Services;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {

            services.AddScoped<SqlExceptionsHandler>();
     

            services.AddScoped<IPasswordHasher,PasswordHasher>();

            // Pour l'utilisateur : Une seule instance pour les deux interfaces
            services.AddScoped<UserRepository>();
            services.AddScoped<IUserCommandRepository>(sp => sp.GetRequiredService<UserRepository>());
            services.AddScoped<IUserQueryRepository>(sp => sp.GetRequiredService<UserRepository>());            


            // Pour les rôles
            services.AddScoped<RoleRepository>();
            services.AddScoped<IRolesCommandRepository>(sp => sp.GetRequiredService<RoleRepository>());
            services.AddScoped<IRolesQueryRepository>(sp => sp.GetRequiredService<RoleRepository>());

            // pour les UserRoles
            services.AddScoped<UserRolesRepository>();
            services.AddScoped<IUserRolesCommandRepository>(sp => sp.GetRequiredService<UserRolesRepository>());
            services.AddScoped<IUserRolesQueryRepository>(sp => sp.GetRequiredService<UserRolesRepository>());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IDapperConnection, DapperContext>(); 



            return services;
        }

      

        }
}
