using MagniseCryptocurrenciesApp.Common.DTOs.AssetDTOs;
using MagniseCryptocurrenciesApp.DataAccess.EntitesModel;
using MagniseCryptocurrenciesApp.Repositories.Interfaces;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MagniseCryptocurrenciesApp.Services.Implementations
{
    public class AssetsService : IAssetsService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public string[] GerAllAssetsId()
        {
            return _assetRepository.GerAllAssetsId();
        }

        public void StoreAssets(List<CoinAPI.REST.V1.Asset> assets)
        {
            var dbAssets = _assetRepository.GetAll();

            var assetsToAdd = new List<Asset>();
            var assetsToUpdate = new List<Asset>();

            // Divides the collection into 10 parts.
            var assetGroups = assets.Select((item, index) => new { index, item })
                       .GroupBy(x => x.index % 10)
                       .Select(x => x.Select(y => y.item));

            var tasks = new List<Task>();
            var assetGroupsMutex = new Mutex();

            foreach (var assetGroup in assetGroups)
            {
                tasks.Add(Task.Run(() => {
                    foreach (var asset in assetGroup)
                    {
                        var dbAsset = dbAssets.FirstOrDefault(a =>
                        a.Id == asset.asset_id);

                        assetGroupsMutex.WaitOne();
                        if (dbAsset != null)
                        {
                            UpdateAsset(asset, dbAsset);
                            assetsToUpdate.Add(dbAsset);
                        }
                        else
                        {
                            assetsToAdd.Add(MapAsset(asset));
                        }
                        assetGroupsMutex.ReleaseMutex();
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            AddAssets(assetsToAdd);
            UpdateAssets(assetsToUpdate);
        }

        public AssetDTO GetAsset(string assetId)
        {
            var asset = _assetRepository
                .Get(asset => asset.Id == assetId);

            return MapAssetDTO(asset);
        }

        public List<AssetDTO> GetAssets(List<string> assetsId)
        {
            return _assetRepository
                .GetAll(asset => assetsId.Contains(asset.Id))
                .Select(asset => MapAssetDTO(asset))
                .ToList();
        }


        private void AddAssets(List<Asset> assets)
        {
            _assetRepository.AddRange(assets);
        }

        private void UpdateAssets(List<Asset> assets)
        {
            _assetRepository.UpdateRange(assets);
        }

        private Asset MapAsset(CoinAPI.REST.V1.Asset asset)
        {
            return new Asset()
            {
                Id = asset.asset_id,
                Name = asset.name,
                PriceUSD = asset.price_usd,
                TypeIsCrypto = asset.type_is_crypto
            };
        }

        private AssetDTO MapAssetDTO(Asset asset)
        {
            return new AssetDTO()
            {
                Id = asset.Id,
                Name = asset.Name,
                PriceUSD = asset.PriceUSD,
                TypeIsCrypto = asset.TypeIsCrypto,
                ModifiedDate = asset.ModifiedDate
            };
        }

        private void UpdateAsset(CoinAPI.REST.V1.Asset asset, Asset assetToUpdate)
        {
            assetToUpdate.Name = asset.name;
            assetToUpdate.PriceUSD = asset.price_usd;
            assetToUpdate.TypeIsCrypto = asset.type_is_crypto;
            assetToUpdate.ModifiedDate = DateTime.UtcNow;
        }
    }
}
