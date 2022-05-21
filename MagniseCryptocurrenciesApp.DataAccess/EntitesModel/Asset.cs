﻿
using System;
using System.Collections.Generic;

namespace MagniseCryptocurrenciesApp.DataAccess.EntitesModel
{
    public class Asset
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal? PriceUSD { get; set; }

        public bool TypeIsCrypto { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<AssetRate> AssetRates { get; set; }
    }
}
