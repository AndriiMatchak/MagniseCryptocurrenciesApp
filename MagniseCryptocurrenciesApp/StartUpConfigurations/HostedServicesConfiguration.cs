using MagniseCryptocurrenciesApp.HostedServices.HostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace MagniseCryptocurrenciesApp.StartUpConfigurations
{
    public class HostedServicesConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddHostedService<AssetsHostedService>();
        }
    }
}
