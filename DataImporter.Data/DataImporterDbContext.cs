using DataImporter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Data
{
    public class DataImporterDbContext : DbContext
    {
        public DataImporterDbContext(DbContextOptions<DataImporterDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasKey(p => new { p.CompanyId, p.FeedId, p.UniqueId });
        }

        public DbSet<CompanyEntity> CompanyEntity { get; set; }
        public DbSet<FeedEntity> FeedEntity { get; set; }
        public DbSet<ProductEntity> ProductEntity { get; set; }
    }
}
