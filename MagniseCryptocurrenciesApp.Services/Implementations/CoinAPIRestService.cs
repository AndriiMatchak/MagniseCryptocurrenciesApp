using CoinAPI.REST.V1;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.Services.Implementations
{
    public class CoinAPIRestService : ICoinAPIRestService
    {
        private readonly CoinApiRestClient coinApiRestClient;
        private readonly IConfigurationManagerService _configurationManager;

        public CoinAPIRestService(IConfigurationManagerService configurationManager)
        {
            _configurationManager = configurationManager;
            coinApiRestClient = InitClient();
        }

        public Task<List<Asset>> GetAllowedAssets()
        {
            return coinApiRestClient.Metadata_list_assetsAsync();
        }

        public CoinApiRestClient InitClient()
        {
            return new CoinApiRestClient(_configurationManager.GetCoinApiKey());
        }
    }
}
