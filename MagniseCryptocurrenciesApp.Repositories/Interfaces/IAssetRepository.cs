using MagniseCryptocurrenciesApp.DataAccess.EntitesModel;

namespace MagniseCryptocurrenciesApp.Repositories.Interfaces
{
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        string[] GerAllAssetsId();
    }
}
