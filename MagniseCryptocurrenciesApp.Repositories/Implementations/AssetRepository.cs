using MagniseCryptocurrenciesApp.DataAccess;
using MagniseCryptocurrenciesApp.DataAccess.EntitesModel;
using MagniseCryptocurrenciesApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagniseCryptocurrenciesApp.Repositories.Implementations
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AssetRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public string[] GerAllAssetsId()
        {
            return _dbContext.Assets.Select(asset => asset.Id).ToArray();
        }
    }
}
