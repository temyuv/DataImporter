using DataImporter.Core.Abstractions;
using DataImporter.Data.Entities;
using DataImporter.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataImporter.Core
{
    public class FeedService : IFeedService
    {
        private readonly IFeedRepository _feedRepository;

        public FeedService(IFeedRepository feedRepository)
        {
            _feedRepository = feedRepository;
        }

        public async Task<FeedEntity> AddFeed(FeedEntity feedEntity)
        {
            return await _feedRepository.Add(feedEntity);
        }

        public async Task<FeedEntity> GetFeedByName(string feedName)
        {
            return await _feedRepository.FirstOrDefault(a => a.Name == feedName);
        }
    }
}
