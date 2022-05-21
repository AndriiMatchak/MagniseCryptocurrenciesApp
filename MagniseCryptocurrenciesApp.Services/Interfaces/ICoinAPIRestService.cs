using CoinAPI.REST.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface ICoinAPIRestService
    {
        public Task<List<Asset>> GetAllAssetsAsync();

        public Task<ExchangeCurrentrate> GetAssetRatesAsync(string assetId);
    }
}
