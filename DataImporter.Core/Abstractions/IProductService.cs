using DataImporter.ApiModels;
using DataImporter.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataImporter.Core.Abstractions
{
    public interface IProductService
    {
        Task<ProductEntity> InsertProduct(ProductEntity productEntity);

        Task<IEnumerable<Product>> GetProductByCompanyIdandFeedId(long companyId, long feedId);
    }
}
