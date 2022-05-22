
namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface IConfigurationManagerService
    {
        string GetCoinApiKey();

        int GetItemsCountToRatesUpdating();
    }
}
