using AutoMapper;
using DataImporter.ApiModels;
using DataImporter.Core;
using DataImporter.Core.Abstractions;
using DataImporter.Data;
using DataImporter.Data.Interfaces;
using DataImporter.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DataImporter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started!");

            Console.WriteLine("Reading Configuration");
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var fileStorageLocation = configuration["FileStorageLocation"];


            Console.WriteLine("Setting up DI");

            //Automapper Configuration
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();


            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<ICompanyRepository, CompanyRepository>()
                .AddScoped<IFeedRepository, FeedRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IFileManager, FileManager>()

                .AddScoped<ICompanyService, CompanyService>()
                .AddScoped<IFeedService, FeedService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IDataImporterService, DataImporterService>()

                .AddSingleton(mapper)

                .AddDbContext<DataImporterDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient)
                .BuildServiceProvider();


            var dataImporterService = serviceProvider.GetService<IDataImporterService>();

            Console.WriteLine("Started Produt Import");

            if (dataImporterService != null)
            {
                dataImporterService.ProductImporter(fileStorageLocation);
            }
            else
            {
                Console.WriteLine("IDataImporterService initialization failed.");
            }

            Console.WriteLine("Completed");

            Console.ReadKey();
        }
    }
}
