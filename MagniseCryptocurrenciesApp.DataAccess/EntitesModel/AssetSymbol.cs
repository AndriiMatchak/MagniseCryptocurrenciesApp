using System;

namespace MagniseCryptocurrenciesApp.DataAccess.EntitesModel
{
    public class AssetSymbol
    {
        public string Id { get; set; }

        public decimal? Price { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Type { get; set; }

        public string AssetId { get; set; }

        public string AssetQuoteId { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Asset Quote { get; set; }
    }
}
