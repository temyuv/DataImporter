using DataImporter.Data.Entities;
using DataImporter.Data.Interfaces;

namespace DataImporter.Data.Repositories
{
    public class CompanyRepository : GenericRepository<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(DataImporterDbContext dbContext) : base(dbContext)
        {

        }
    }
}
