using System;
using System.Collections.Generic;
using System.Text;

namespace MagniseCryptocurrenciesApp.Common.DTOs.AssetDTOs
{
    public class AssetDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal? PriceUSD { get; set; }

        public bool TypeIsCrypto { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
