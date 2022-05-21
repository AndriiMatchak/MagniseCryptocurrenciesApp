using MagniseCryptocurrenciesApp.Common.DTOs.AssetRateDTOs;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.Common.Models.ViewModels
{
    public class AssetsRatesPriceInfoViewModel
    {
        public string AssetId { get; set; }

        public List<AssetRateDTO> AssetRates { get; set; }
    }
}
