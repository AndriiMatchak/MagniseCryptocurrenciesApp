using MagniseCryptocurrenciesApp.DataAccess.EntitesModel;
using Microsoft.EntityFrameworkCore;

namespace MagniseCryptocurrenciesApp.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        #region Entities

        public virtual DbSet<Asset> Assets { get; set; }

        public virtual DbSet<AssetRate> AssetRates { get; set; }

        public virtual DbSet<AssetSymbol> AssetSymbols { get; set; }

        #endregion

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasMany(a => a.AssetRates)
                .WithOne(ar => ar.Asset)
                .HasForeignKey(ar => ar.AssetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asset>()
               .HasMany(a => a.QuoteRates)
               .WithOne(ar => ar.Quote)
               .HasForeignKey(ar => ar.AssetQuoteId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssetRate>().HasKey(ar => ar.Id);
            modelBuilder.Entity<AssetRate>().HasIndex(ar =>
            new { ar.AssetId, ar.AssetQuoteId }).IsUnique();

            modelBuilder.Entity<Asset>()
                .HasMany(a => a.AssetSymbols)
                .WithOne(ar => ar.Asset)
                .HasForeignKey(ar => ar.AssetId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asset>()
               .HasMany(a => a.QuoteSymbols)
               .WithOne(ar => ar.Quote)
               .HasForeignKey(ar => ar.AssetQuoteId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AssetSymbol>().HasKey(ar => ar.Id);
            modelBuilder.Entity<AssetSymbol>().HasIndex(ar =>
            new { ar.AssetId, ar.AssetQuoteId }).IsUnique(false);
        }

        #endregion
    }
}
