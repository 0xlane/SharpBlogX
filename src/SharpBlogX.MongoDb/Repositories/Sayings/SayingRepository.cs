using SharpBlogX.Domain.Sayings;
using SharpBlogX.Domain.Sayings.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.MongoDB;

namespace SharpBlogX.Repositories.Sayings
{
    public class SayingRepository : MongoDbRepositoryBase<Saying>, ISayingRepository
    {
        public SayingRepository(IMongoDbContextProvider<SharpBlogXMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Tuple<int, List<Saying>>> GetPagedListAsync(int skipCount, int maxResultCount)
        {
            var filter = new BsonDocument();
            var total = await Collection.CountDocumentsAsync(filter);
            var list = await Collection.Find(filter)
                                       .Skip((skipCount - 1) * maxResultCount)
                                       .Limit(maxResultCount)
                                       .ToListAsync();
            return new Tuple<int, List<Saying>>((int)total, list);
        }

        public async Task<Saying> GetRandomAsync()
        {
            return await Collection.AsQueryable().Sample(1).FirstOrDefaultAsync();
        }
    }
}