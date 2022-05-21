using CoinAPI.REST.V1;
using CoinAPI.WebSocket.V1.DataModels;
using MagniseCryptocurrenciesApp.Common.DTOs.AssetRateDTOs;
using MagniseCryptocurrenciesApp.Common.Models.ViewModels;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface IAssetRateService
    {
        void StoreRatesTable(Dictionary<string, ExchangeRate> ratesTable);

        void StoreRates(Rate[] rates, string assetId);

        List<AssetRateDTO> GetAssetPriceInfo(string assetId);

        List<AssetsRatesPriceInfoViewModel> GetAssetsPriceInfo(List<string> assetsId);
    }
}
