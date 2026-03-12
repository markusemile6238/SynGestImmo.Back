using Microsoft.Extensions.DependencyInjection;

namespace Tools.Security
{
    public static class AuthorizationPolicies
    {

        public const string MustChangePassword = "MustChangePassword";

        public static void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(MustChangePassword, policy =>
                    policy.RequireClaim("mustChangePassword", "false"));
            });
        }

    }
}
