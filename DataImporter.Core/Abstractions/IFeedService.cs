using DataImporter.Data.Entities;
using System.Threading.Tasks;

namespace DataImporter.Core.Abstractions
{
    public interface IFeedService
    {
        Task<FeedEntity> AddFeed(FeedEntity feedEntity);
        Task<FeedEntity> GetFeedByName(string feedName);
    }
}