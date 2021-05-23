using DataImporter.Data.Entities;
using DataImporter.Data.Interfaces;

namespace DataImporter.Data.Repositories
{

    public class FeedRepository : GenericRepository<FeedEntity>, IFeedRepository
    {
        public FeedRepository(DataImporterDbContext dbContext) : base(dbContext)
        {

        }
    }
}
