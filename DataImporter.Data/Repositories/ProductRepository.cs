using DataImporter.Data.Entities;
using DataImporter.Data.Interfaces;

namespace DataImporter.Data.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(DataImporterDbContext dbContext) : base(dbContext)
        {
        }
    }
}
