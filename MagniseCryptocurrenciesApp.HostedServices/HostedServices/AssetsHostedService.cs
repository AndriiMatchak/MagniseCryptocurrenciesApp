using MagniseCryptocurrenciesApp.HostedServices.BaseServices;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var coinAPIRestService = scope.ServiceProvider.GetService<ICoinAPIRestService>();
                var assetService = scope.ServiceProvider.GetService<IAssetsService>();

                var assets = await coinAPIRestService.GetAllowedAssets();

                await Task.Run(() => assetService.StoreAssets(assets)).ConfigureAwait(false);

                await Task.Run(() => _coinAPIWsService.ReadCryptoCurrenciesData()).ConfigureAwait(false);
            }
        }
    }
}
