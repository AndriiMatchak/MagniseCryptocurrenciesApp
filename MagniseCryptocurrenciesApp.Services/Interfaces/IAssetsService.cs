using CoinAPI.REST.V1;
using CoinAPI.WebSocket.V1.DataModels;
using MagniseCryptocurrenciesApp.Common.DTOs.AssetDTOs;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface IAssetsService
    {
        void StoreAssets(List<Asset> assets);

        string[] GerAllAssetsId();

        AssetDTO GetAsset(string assetId);

        List<AssetDTO> GetAssets(List<string> assetsId);
    }
}
