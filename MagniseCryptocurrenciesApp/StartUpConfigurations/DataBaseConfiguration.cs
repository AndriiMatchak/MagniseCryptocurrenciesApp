using MagniseCryptocurrenciesApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MagniseCryptocurrenciesApp.StartUpConfigurations
{
    public class DataBaseConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("defaultconnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
        }
    }
}
