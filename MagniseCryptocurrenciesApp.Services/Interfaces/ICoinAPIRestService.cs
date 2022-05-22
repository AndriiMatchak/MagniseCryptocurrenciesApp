using CoinAPI.REST.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface ICoinAPIRestService
    {
        Task<List<Asset>> GetAllAssetsAsync();

        Task<ExchangeCurrentrate> GetAssetRatesAsync(string assetId);

        Task<List<Symbol>> GetAllSymbolsAsync();
    }
}
