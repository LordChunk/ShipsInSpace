using System;
using System.Threading.Tasks;
using AutoMapper;
using GalacticSpaceTransitAuthority;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShipsInSpace.Models;
using ShipsInSpace.Models.Enums;

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

                options.AddPolicy("License", policy => policy.RequireClaim("License"));

                foreach (var license in Enum.GetValues<PilotLicense>())
                {
                    options.AddPolicy("License "+license, policy => policy.RequireClaim("License", license.ToString()));
                }
            });
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Engine, EngineViewModel>();
                cfg.CreateMap<Hull, HullViewModel>();
                cfg.CreateMap<ShipViewModel, Ship>();
                cfg.CreateMap<Weapon, WeaponViewModel>();
                cfg.CreateMap<Wing, WingViewModel>();
            });

            var mapper = new Mapper(config);

            services.AddSingleton(mapper);
        }
    }
}