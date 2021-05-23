using CsvHelper.Configuration;
using DataImporter.Data.Entities;

namespace DataImporter.Core
{
    public class ProductFileMap : ClassMap<ProductEntity>
    {
        public ProductFileMap()
        {
            //TODO: Move the field mapping names to app settings
            Map(m => m.UniqueId).Name("Unique Id");
            Map(m => m.Name).Name("Name");
            Map(m => m.Brand).Name("Brand");
            Map(m => m.Description).Name("Description");
            Map(m => m.CompanyId).Ignore();
            Map(m => m.FeedId).Ignore();
        }
    }
}
