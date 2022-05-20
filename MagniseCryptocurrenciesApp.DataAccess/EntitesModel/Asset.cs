
using System;

namespace MagniseCryptocurrenciesApp.DataAccess.EntitesModel
{
    public class Asset
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal? PriceUSD { get; set; }

        public bool TypeIsCrypto { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
