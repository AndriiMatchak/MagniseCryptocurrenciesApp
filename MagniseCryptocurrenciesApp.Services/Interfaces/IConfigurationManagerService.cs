
namespace MagniseCryptocurrenciesApp.Services.Interfaces
{
    public interface IConfigurationManagerService
    {
        public string GetCoinApiKey();

        public int GetItemsCountToRatesUpdating();
    }
}
