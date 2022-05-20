using CoinAPI.REST.V1;
using CoinAPI.WebSocket.V1.DataModels;
using MagniseCryptocurrenciesApp.Common.DTOs.AssetDTOs;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface IAssetsService
    {
        public void StoreAssets(List<Asset> assets);

        public void StoreRatesTable(Dictionary<string, ExchangeRate> ratesTable);

        public string[] GerAllAssetsId();

        public List<AssetDTO> GetAllAssets();

        public AssetDTO GetAssetPriceInfo(string assetId);

        public List<AssetDTO> GetAssetsPriceInfo(List<string> assetsId);
    }
}
