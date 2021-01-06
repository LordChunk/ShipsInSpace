using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShipsInSpace.Models;

namespace ShipsInSpace
{
    public static class ConfigureServices
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roles = new[] { "Manager" };
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role(role));
                }
            }
        }


        public static void AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
            });
        }
    }
}