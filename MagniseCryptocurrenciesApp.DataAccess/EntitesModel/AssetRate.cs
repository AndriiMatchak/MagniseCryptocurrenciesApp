using System;

namespace MagniseCryptocurrenciesApp.DataAccess.EntitesModel
{
    public class AssetRate
    {
        public DateTime ModifiedDate { get; set; }

        public string AssetId { get; set; }

        public string AssetIdQuote { get; set; }

        public decimal Rate { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
