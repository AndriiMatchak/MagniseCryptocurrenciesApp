using MagniseCryptocurrenciesApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace MagniseCryptocurrenciesApp.Services.Implementations
{
    public class ConfigurationManagerService : IConfigurationManagerService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCoinApiKey()
        {
            return _configuration["CoinApiKey"];
        }

        public int GetItemsCountToRatesUpdating()
        {
            return Convert.ToInt32(_configuration["ItemsCountToRatesUpdating"]);
        }
    }
}
