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

        #endregion

        #region ModelBuilder

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        #endregion
    }
}
