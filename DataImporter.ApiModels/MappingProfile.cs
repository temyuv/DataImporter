using AutoMapper;
using DataImporter.Data.Entities;

namespace DataImporter.ApiModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductEntity>();
            CreateMap<ProductEntity, Product>();
        }
    }
}
