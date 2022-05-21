using CoinAPI.REST.V1;
using CoinAPI.WebSocket.V1.DataModels;
using MagniseCryptocurrenciesApp.Common.DTOs.AssetRateDTOs;
using MagniseCryptocurrenciesApp.Common.Models.ViewModels;
using MagniseCryptocurrenciesApp.DataAccess.EntitesModel;
using MagniseCryptocurrenciesApp.Repositories.Interfaces;
using MagniseCryptocurrenciesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagniseCryptocurrenciesApp.Services.Implementations
{
    public class AssetRateService : IAssetRateService
    {
        private readonly IBaseRepository<AssetRate> _assetRateRepository;

        public AssetRateService(IBaseRepository<AssetRate> assetRateRepository)
        {
            _assetRateRepository = assetRateRepository;
        }

        public void StoreRates(Rate[] rates, string assetId)
        {
            var dbAssetRates = _assetRateRepository.GetAll(rate =>
            rate.AssetId == assetId);

            var assetRatesToAdd = new List<AssetRate>();
            var assetRatesToUpdate = new List<AssetRate>();

            foreach (var rate in rates)
            {
                var dbAssetRate = dbAssetRates.FirstOrDefault(r =>
                r.AssetIdQuote == rate.asset_id_quote);

                if (dbAssetRate != null)
                {
                    UpdateAssetRate(rate, dbAssetRate);
                    assetRatesToUpdate.Add(dbAssetRate);
                }
                else
                {
                    assetRatesToAdd.Add(MapAssetRate(rate, assetId));
                }
            }

            AddAssetRates(assetRatesToAdd);
            UpdateAssetRates(assetRatesToUpdate);
        }

        public void StoreRatesTable(Dictionary<string, ExchangeRate> ratesTable)
        {
            var assetRatesToUpdate = _assetRateRepository.GetAll(assertRate =>
                ratesTable.Select(rate => rate.Value.asset_id_base).Contains(assertRate.AssetId));

            foreach (var assetRate in assetRatesToUpdate)
            {
                var assetRateKey = assetRate.AssetId + assetRate;

                if(ratesTable.ContainsKey(assetRateKey))
                    UpdateAssetRate(ratesTable[assetRateKey], assetRate);
            }

            _assetRateRepository.UpdateRange(assetRatesToUpdate);
        }

        public List<AssetRateDTO> GetAssetPriceInfo(string assetId)
        {
            return _assetRateRepository.GetAll(ar => ar.AssetId == assetId)
                .Select(ar => MapAssetRate(ar)).ToList();
        }

        public List<AssetsRatesPriceInfoViewModel> GetAssetsPriceInfo(List<string> assetsId)
        {
            return _assetRateRepository.GetAll(ar => assetsId.Contains(ar.AssetId))
                .GroupBy(ar => ar.AssetId).Select(arGroup =>
                new AssetsRatesPriceInfoViewModel() {
                    AssetId = arGroup.Key,
                    AssetRates = arGroup.Select(ar => MapAssetRate(ar)).ToList()
                }).ToList();
        }

        private void UpdateAssetRate(Rate rate, AssetRate assetRateToUpdate)
        {
            assetRateToUpdate.Rate = rate.rate;
            assetRateToUpdate.ModifiedDate = rate.time;
        }

        private void UpdateAssetRate(ExchangeRate rate, AssetRate assetRateToUpdate)
        {
            assetRateToUpdate.Rate = rate.rate;
            assetRateToUpdate.ModifiedDate = rate.time;
        }

        private AssetRate MapAssetRate(Rate rate, string assetId)
        {
            return new AssetRate()
            {
                AssetId = assetId,
                AssetIdQuote = rate.asset_id_quote,
                Rate = rate.rate,
                ModifiedDate = rate.time
            };
        }

        private AssetRateDTO MapAssetRate(AssetRate assetRate)
        {
            return new AssetRateDTO()
            {
                AssetId = assetRate.AssetId,
                AssetIdQuote = assetRate.AssetIdQuote,
                Rate = assetRate.Rate,
                ModifiedDate = assetRate.ModifiedDate
            };
        }

        private void AddAssetRates(List<AssetRate> assetRates)
        {
            _assetRateRepository.AddRange(assetRates);
        }

        private void UpdateAssetRates(List<AssetRate> assetRates)
        {
            _assetRateRepository.UpdateRange(assetRates);
        }
    }
}
