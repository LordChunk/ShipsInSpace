using Microsoft.Extensions.DependencyInjection;

namespace ShipsInSpace
{
    public static class ConfigureServices
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
            });
        }
    }
}