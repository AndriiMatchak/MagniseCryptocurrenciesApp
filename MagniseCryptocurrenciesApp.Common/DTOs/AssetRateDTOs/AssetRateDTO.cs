using System;

namespace MagniseCryptocurrenciesApp.Common.DTOs.AssetRateDTOs
{
    public class AssetRateDTO
    {
        public DateTime ModifiedDate { get; set; }

        public string AssetId { get; set; }

        public string AssetIdQuote { get; set; }

        public decimal Rate { get; set; }
    }
}
