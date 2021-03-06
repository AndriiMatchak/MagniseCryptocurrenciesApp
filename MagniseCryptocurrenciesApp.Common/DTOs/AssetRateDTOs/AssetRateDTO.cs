using System;

namespace MagniseCryptocurrenciesApp.Common.DTOs.AssetRateDTOs
{
    public class AssetRateDTO
    {
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string AssetId { get; set; }

        public string AssetQuoteId { get; set; }

        public decimal Rate { get; set; }
    }
}
