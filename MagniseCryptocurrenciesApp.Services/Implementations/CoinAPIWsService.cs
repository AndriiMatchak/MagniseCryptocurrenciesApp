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
        private static readonly ManualResetEvent _coinsQuoteResetEvent = new ManualResetEvent(false);

        private int _secondsToTableUpdating;
        private bool _tableUpdatingEnabled = true;

        private readonly IHubContext<AssetsHub> _assetsHubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly Dictionary<string, ExchangeRate> _ratesDictionary = new Dictionary<string, ExchangeRate>();

        public CoinAPIWsService(IHubContext<AssetsHub> assetsHubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _assetsHubContext = assetsHubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ReadCryptoCurrenciesData()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var configurationManager = scope.ServiceProvider.GetService<IConfigurationManagerService>();
                    var assetsService = scope.ServiceProvider.GetService<IAssetsService>();

                    _secondsToTableUpdating = configurationManager.GetSecondsToRatesUpdating();

                    var subscribeAssetsid = assetsService.GerAllAssetsId();

                    using (var coinApiWsClient = InitClient())
                    {
                        SendHelloMessage(coinApiWsClient, subscribeAssetsid, configurationManager);

                        Task.Run(() => StoreRatesTableProcess()).ConfigureAwait(false);

                        _coinsQuoteResetEvent.WaitOne();
                    }
                }
            }
            catch(Exception ex)
            {
                _coinsQuoteResetEvent.Reset();
                ReadCryptoCurrenciesData();
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
        }

        private void StoreRateInRatesTable(ExchangeRate rate)
        {
            if (_ratesDictionary.ContainsKey(rate.asset_id_base))
            {
                _ratesDictionary[rate.asset_id_base] = rate;
            }
            else
            {
                _ratesDictionary.Add(rate.asset_id_base, rate);
            }
        }

        private void StoreRatesTableProcess()
        {
            while (_tableUpdatingEnabled)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var assetsService = scope.ServiceProvider.GetService<IAssetsService>();

                    assetsService.StoreRatesTable(_ratesDictionary);
                }

                Task.Delay(TimeSpan.FromSeconds(_secondsToTableUpdating)).Wait();
            }
        }
    }
}
