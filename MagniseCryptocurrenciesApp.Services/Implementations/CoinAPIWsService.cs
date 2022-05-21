using CoinAPI.WebSocket.V1;
using CoinAPI.WebSocket.V1.DataModels;
using MagniseCryptocurrenciesApp.Hubs.SignalRHubs;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.Services.Implementations
{
    public class CoinAPIWsService : ICoinAPIWsService
    {
        private static readonly ManualResetEvent _coinsQuoteResetEvent = 
            new ManualResetEvent(false);
        private readonly Dictionary<string, ExchangeRate> _ratesDictionary =
            new Dictionary<string, ExchangeRate>();

        private int _itemsCountToTableUpdating;

        private readonly IHubContext<AssetsHub> _assetsHubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CoinAPIWsService(IHubContext<AssetsHub> assetsHubContext,
            IServiceScopeFactory serviceScopeFactory)
        {
            _assetsHubContext = assetsHubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ReadAssetsRateData()
        {
            try
            {
                var scope = _serviceScopeFactory.CreateScope();
                var configurationManager = scope.ServiceProvider.GetService<IConfigurationManagerService>();
                var assetsService = scope.ServiceProvider.GetService<IAssetsService>();

                _itemsCountToTableUpdating = configurationManager.GetItemsCountToRatesUpdating();

                var subscribeAssetsid = assetsService.GerAllAssetsId();

                using (var coinApiWsClient = InitClient())
                {
                    SendHelloMessage(coinApiWsClient, subscribeAssetsid, configurationManager);

                    scope.Dispose();

                    Task.Run(() => StoreRatesTableProcess()).ConfigureAwait(false);

                    _coinsQuoteResetEvent.WaitOne();
                }
            }
            catch(Exception ex)
            {
                _coinsQuoteResetEvent.Reset();
                ReadAssetsRateData();
            }
        }

        private CoinApiWsClient InitClient()
        {
            var coinApiWsClient = new CoinApiWsClient(true);

            AddEventHandlers(coinApiWsClient);

            return coinApiWsClient;
        }

        private void AddEventHandlers(CoinApiWsClient coinApiWsClient)
        {
            coinApiWsClient.ExchangeRateEvent += RateHandler;
            coinApiWsClient.Error += ExceptionsHandler;
        }

        private void SendHelloMessage(CoinApiWsClient coinApiWsClient,
            string[] subscribeAssetsid,
            IConfigurationManagerService configurationManager)
        {
            var helloMsg = new Hello()
            {
                apikey = Guid.Parse(configurationManager.GetCoinApiKey()),
                subscribe_data_type = new string[] { "exrate" },
                subscribe_filter_asset_id = subscribeAssetsid
            };

            coinApiWsClient.SendHelloMessage(helloMsg);
        }

        private async void RateHandler(object sender, ExchangeRate rate)
        {
            StoreRateInRatesTable(rate);

            await _assetsHubContext.Clients.All.SendAsync("ChangeRate", rate).ConfigureAwait(false);

            if (_ratesDictionary.Count >= _itemsCountToTableUpdating)
                StoreRatesTableProcess();
        }

        private void ExceptionsHandler(object sender, Exception ex)
        {
            _coinsQuoteResetEvent.Reset();
            ReadAssetsRateData();
        }

        private void StoreRateInRatesTable(ExchangeRate rate)
        {
            var rateKey = rate.asset_id_base + rate.asset_id_quote;

            if (_ratesDictionary.ContainsKey(rateKey))
            {
                _ratesDictionary[rateKey] = rate;
            }
            else
            {
                _ratesDictionary.Add(rateKey, rate);
            }
        }

        private void StoreRatesTableProcess()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var assetRateService = scope.ServiceProvider.GetService<IAssetRateService>();

                assetRateService.StoreRatesTable(_ratesDictionary);
            }

            _ratesDictionary.Clear();
        }
    }
}
