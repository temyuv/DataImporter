using AutoMapper;
using DataImporter.ApiModels;
using DataImporter.Core.Abstractions;
using DataImporter.Data.Entities;
using DataImporter.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Core
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<ProductEntity> InsertProduct(ProductEntity productEntity)
        {
            return await _ProductRepository.Add(productEntity);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productEntities = await _ProductRepository.GetAll();

            var products = _mapper.Map<List<Product>>(productEntities.ToList());

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByCompanyIdandFeedId(long companyId, long feedId)
        {
            var productEntities = await _ProductRepository.GetWhere(a => a.CompanyId == companyId && a.FeedId == feedId);

            var products = _mapper.Map<List<Product>>(productEntities.ToList());

            return products;
        }
    }
}
