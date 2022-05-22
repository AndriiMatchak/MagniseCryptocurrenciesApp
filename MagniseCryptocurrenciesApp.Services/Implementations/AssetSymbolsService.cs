using CoinAPI.REST.V1;
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
    public class AssetSymbolsService : IAssetSymbolsService
    {
        private readonly IBaseRepository<AssetSymbol> _assetSymbolsRepository;
        private readonly IAssetsService _assetsService;

        public AssetSymbolsService(IBaseRepository<AssetSymbol> assetSymbolsRepository,
            IAssetsService assetsService)
        {
            _assetSymbolsRepository = assetSymbolsRepository;
            _assetsService = assetsService;
        }

        public void StoreAssetSymbols(List<Symbol> symbols)
        {
            var dbAssetSymbols = _assetSymbolsRepository.GetAll();
            var dbAssetsId = _assetsService.GerAllAssetsId();

            symbols = symbols.Where(s => dbAssetsId.Contains(s.asset_id_base) &&
            dbAssetsId.Contains(s.asset_id_quote)).ToList();

            var assetSymbolsToAdd = new List<AssetSymbol>();
            var assetSymbolsToUpdate = new List<AssetSymbol>();

            // Divides the collection into 10 parts.
            var assetSymbolsGroups = symbols.Select((item, index) => new { index, item })
                       .GroupBy(x => x.index % 10)
                       .Select(x => x.Select(y => y.item));

            var tasks = new List<Task>();
            var assetSymbolsGroupsMutex = new Mutex();

            foreach (var assetSymbolsGroup in assetSymbolsGroups)
            {
                tasks.Add(Task.Run(() => {
                    foreach (var symbol in assetSymbolsGroup)
                    {
                        var dbAssetSymbol = dbAssetSymbols.FirstOrDefault(a =>
                        a.AssetId == symbol.asset_id_base &&
                        a.AssetQuoteId == symbol.asset_id_quote);

                        assetSymbolsGroupsMutex.WaitOne();
                        if (dbAssetSymbol != null)
                        {
                            UpdateAssetSymbol(symbol, dbAssetSymbol);
                            assetSymbolsToUpdate.Add(dbAssetSymbol);
                        }
                        else
                        {
                            assetSymbolsToAdd.Add(MapAssetSymbol(symbol));
                        }
                        assetSymbolsGroupsMutex.ReleaseMutex();
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            AddAssetSymbols(assetSymbolsToAdd);
            UpdateAssetSymbols(assetSymbolsToUpdate);
        }

        private void UpdateAssetSymbol(Symbol symbol, AssetSymbol symbolToUpdate)
        {
            symbolToUpdate.Price = symbol.price;
            symbolToUpdate.ModifiedDate = DateTime.UtcNow;
        }

        private void AddAssetSymbols(List<AssetSymbol> assetSymbols)
        {
            _assetSymbolsRepository.AddRange(assetSymbols);
        }

        private void UpdateAssetSymbols(List<AssetSymbol> assetSymbols)
        {
            _assetSymbolsRepository.UpdateRange(assetSymbols);
        }

        private AssetSymbol MapAssetSymbol(Symbol symbol)
        {
            return new AssetSymbol()
            {
                Id = symbol.symbol_id,
                Price = symbol.price,
                AssetId = symbol.asset_id_base,
                AssetQuoteId = symbol.asset_id_quote,
                Type = symbol.symbol_type,
                ModifiedDate = DateTime.UtcNow
            };
        }
    }
}
