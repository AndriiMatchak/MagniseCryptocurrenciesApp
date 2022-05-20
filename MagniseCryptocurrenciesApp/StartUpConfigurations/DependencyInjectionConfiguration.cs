﻿using MagniseCryptocurrenciesApp.HostedServices.HostedServices;
using MagniseCryptocurrenciesApp.Repositories.Implementations;
using MagniseCryptocurrenciesApp.Repositories.Interfaces;
using MagniseCryptocurrenciesApp.Services.Implementations;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MagniseCryptocurrenciesApp.StartUpConfigurations
{
    public class DependencyInjectionConfiguration
    {
        public static void Inject(IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IAssetRepository, AssetRepository>();

            #endregion

            #region Services

            services.AddScoped<IConfigurationManagerService, ConfigurationManagerService>();

            services.AddTransient<IAssetsService, AssetsService>();

            services.AddScoped<ICoinAPIRestService, CoinAPIRestService>();

            services.AddSingleton<ICoinAPIWsService, CoinAPIWsService>(); 

            #endregion

            #region HostedServices

            services.AddSingleton<AssetsHostedService>();

            #endregion
        }
    }
}
