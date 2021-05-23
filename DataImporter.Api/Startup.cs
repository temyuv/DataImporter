using AutoMapper;
using DataImporter.ApiModels;
using DataImporter.Core;
using DataImporter.Core.Abstractions;
using DataImporter.Data;
using DataImporter.Data.Interfaces;
using DataImporter.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace DataImporter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataImporterDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Repos
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IFeedRepository, FeedRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();



            //Services
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddControllers().AddNewtonsoftJson();

            //Swagger Configuration
            services.AddSwaggerGen();

            //Automapper Configuration
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //Migrate and Seed Database
            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DataImporterDbContext>();
                    if (!context.Database.EnsureCreated())
                    {
                        DataImporterSeed.SeedAsync(context).Wait();
                    }
                };
            }
            catch (Exception)
            {
                //TODO: Implement logging.
                // logger.LogError(exception, "An error occurred seeding the DB.");
            }
        }
    }
}
