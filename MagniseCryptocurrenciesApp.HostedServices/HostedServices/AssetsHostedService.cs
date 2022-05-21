using CoinAPI.REST.V1;
using MagniseCryptocurrenciesApp.HostedServices.BaseServices;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.HostedServices.HostedServices
{
    public class AssetsHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICoinAPIWsService _coinAPIWsService;

        public AssetsHostedService(IServiceScopeFactory serviceScopeFactory, ICoinAPIWsService coinAPIWsService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _coinAPIWsService = coinAPIWsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StoreCurrentAssetsData().ConfigureAwait(false);
            await StartAssetsDataReading().ConfigureAwait(false);
        }

        private async Task StoreCurrentAssetsData()
        {
            List<Asset> assets;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var coinAPIRestService = scope.ServiceProvider.GetService<ICoinAPIRestService>();
                var assetService = scope.ServiceProvider.GetService<IAssetsService>();
                var assetRateService = scope.ServiceProvider.GetService<IAssetRateService>();

                assets = await coinAPIRestService.GetAllAssetsAsync().ConfigureAwait(false);

                await Task.Run(() => assetService.StoreAssets(assets)).ConfigureAwait(false);
            }

            foreach (var asset in assets)
            {
                await Task.Run(() => StoreAssetRate(asset)).ConfigureAwait(false);
            }
        }

        private Task StartAssetsDataReading()
        {
            return Task.Run(() => _coinAPIWsService.ReadAssetsRateData());
        }

        private async void StoreAssetRate(Asset asset)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var coinAPIRestService = scope.ServiceProvider.GetService<ICoinAPIRestService>();
                var assetRateService = scope.ServiceProvider.GetService<IAssetRateService>();

                var rates = await coinAPIRestService.GetAssetRatesAsync(asset.asset_id)
                .ConfigureAwait(false);

                await Task.Run(() => assetRateService.StoreRates(rates.rates, asset.asset_id));
            }
        }
    }
}
