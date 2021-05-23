using CsvHelper;
using DataImporter.Core.Abstractions;
using DataImporter.Data.Entities;
using System;
using System.Globalization;
using System.IO;

namespace DataImporter.Core
{
    public class DataImporterService : IDataImporterService
    {
        private readonly IProductService _productService;
        private readonly ICompanyService _companyService;
        private readonly IFeedService _feedService;
        private readonly IFileManager _fileManager;

        public DataImporterService(IProductService productService,
                                   ICompanyService companyService,
                                   IFeedService feedService,
                                   IFileManager fileManager
                                   )
        {
            _productService = productService;
            _companyService = companyService;
            _feedService = feedService;
            _fileManager = fileManager;
        }

        public bool ProductImporter(string rootFolderlocation)
        {
            try
            {
                if (Directory.Exists(rootFolderlocation))
                {
                    //TODO: Move file format filter *.csv to configuration file.
                    foreach (var filePath in _fileManager.GetFiles(rootFolderlocation, "*.csv", SearchOption.AllDirectories))
                    {
                        var feedName = Directory.GetParent(filePath).Name;
                        var companyName = Directory.GetParent(Directory.GetParent(filePath).FullName).Name;

                        //TODO:Avoid Repeated db calls.
                        var feed = _feedService.GetFeedByName(feedName).Result;
                        if (feed == null)
                        {
                            feed = _feedService.AddFeed(new FeedEntity { Name = feedName }).Result;
                        }
                        var company = _companyService.GetCompanyByName(companyName).Result;
                        if (company == null)
                        {
                            company = _companyService.AddCompany(new CompanyEntity { Name = companyName }).Result;
                        }

                        using (var reader = _fileManager.StreamReader(filePath))
                        {
                            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                                csv.Context.RegisterClassMap<ProductFileMap>(); //Mapping product file header names with product class property names
                                csv.Read();
                                csv.ReadHeader();
                                if (!csv.Context.Configuration.HasHeaderRecord) return false;
                                while (csv.Read())
                                {
                                    var product = csv.GetRecord<ProductEntity>();
                                    product.CompanyId = company.Id;
                                    product.FeedId = feed.Id;

                                    //TODO: Batching insert records to db and 
                                    //isolate SaveChanges from generic repository
                                    _productService.InsertProduct(product);
                                }
                            }
                        }
                    }

                    //TODO: Archiving file if successful

                    return true;
                }

                //Something went wrong. - TODO:implement logger to log the failures
                return false;
            }
            catch (Exception ex)
            {

                //TODO:Implement logger to log exceptions
                throw ex;
            }
        }
    }
}
