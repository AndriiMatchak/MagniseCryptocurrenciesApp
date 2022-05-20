using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MagniseCryptocurrenciesApp.StartUpConfigurations
{
    public class SwaggerGenConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MagniseCryptocurrenciesApp", Version = "v1" });
            });
        }
    }
}
