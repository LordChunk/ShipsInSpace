using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ShipsInSpace.Areas.Identity.IdentityHostingStartup))]
namespace ShipsInSpace.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}