using System;

namespace MagniseCryptocurrenciesApp.DataAccess.EntitesModel
{
    public class AssetRate
    {
        public Guid Id { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string AssetId { get; set; }

        public string AssetQuoteId { get; set; }

        public decimal Rate { get; set; }

        public virtual Asset Asset { get; set; }

        public virtual Asset Quote { get; set; }
    }
}
