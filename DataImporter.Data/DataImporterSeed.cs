using CsvHelper;
using DataImporter.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace DataImporter.Data
{
    public class DataImporterSeed
    {
        public static async Task SeedAsync(DataImporterDbContext dataImporterDbContext)
        {
            try
            {
                dataImporterDbContext.Database.Migrate();

                if (!dataImporterDbContext.CompanyEntity.Any())
                {
                    dataImporterDbContext.Database.OpenConnection();
                    dataImporterDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Company ON");
                    dataImporterDbContext.CompanyEntity.AddRange(ReadCompanies(dataImporterDbContext, @"C:\Workspace\DataImporter\Company.csv"));
                    await dataImporterDbContext.SaveChangesAsync();
                    dataImporterDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Company OFF");
                }

                if (!dataImporterDbContext.FeedEntity.Any())
                {
                    dataImporterDbContext.Database.OpenConnection();
                    dataImporterDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Feed ON");
                    dataImporterDbContext.FeedEntity.AddRange(ReadFeeds(dataImporterDbContext, @"C:\Workspace\DataImporter\Feed.csv"));
                    await dataImporterDbContext.SaveChangesAsync();
                    dataImporterDbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Feed OFF");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        public static List<CompanyEntity> ReadCompanies(DataImporterDbContext dataImporterDbContext, string companyFilePath)
        {
            List<CompanyEntity> companyEntities = new List<CompanyEntity>();
            using (var reader = new StreamReader(companyFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //csv.Context.RegisterClassMap<ProductFileMap>(); //Mapping product file header names with product class property names
                // csv.ReadHeader();
                while (csv.Read())
                {
                    companyEntities.Add(csv.GetRecord<CompanyEntity>());
                }
            }

            return companyEntities;
        }

        public static List<FeedEntity> ReadFeeds(DataImporterDbContext dataImporterDbContext, string companyFilePath)
        {
            List<FeedEntity> feedEntities = new List<FeedEntity>();

            using (var reader = new StreamReader(companyFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //csv.Context.RegisterClassMap<ProductFileMap>(); //Mapping product file header names with product class property names
                // csv.ReadHeader();
                while (csv.Read())
                {
                    feedEntities.Add(csv.GetRecord<FeedEntity>());
                }
            }

            return feedEntities;
        }
    }
}
