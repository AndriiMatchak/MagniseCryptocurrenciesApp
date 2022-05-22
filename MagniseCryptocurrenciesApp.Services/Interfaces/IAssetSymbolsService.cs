using CoinAPI.REST.V1;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface IAssetSymbolsService
    {
        void StoreAssetSymbols(List<Symbol> symbols);
    }
}
