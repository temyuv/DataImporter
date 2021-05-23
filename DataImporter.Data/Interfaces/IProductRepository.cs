using DataImporter.Data.Entities;
using DataImporter.Data.Repositories;

namespace DataImporter.Data.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
    }
}
