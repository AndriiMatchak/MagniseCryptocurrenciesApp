using MagniseCryptocurrenciesApp.Filters;
using MagniseCryptocurrenciesApp.Hubs.SignalRHubs;
using MagniseCryptocurrenciesApp.StartUpConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MagniseCryptocurrenciesApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));
            services.AddSignalR();

            SwaggerGenConfiguration.Configure(services, Configuration);

            DataBaseConfiguration.Configure(services, Configuration);

            DependencyInjectionConfiguration.Inject(services);

            HostedServicesConfiguration.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MagniseCryptocurrenciesApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AssetsHub>("/AssetsHub");
            });
        }
    }
}
