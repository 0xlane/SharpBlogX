using SharpBlogX.Domain.Blog.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.MongoDB;
using Tag = SharpBlogX.Domain.Blog.Tag;

namespace SharpBlogX.Repositories.Blog
{
    public class TagRepository : MongoDbRepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IMongoDbContextProvider<SharpBlogXMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Tag>> GetListAsync(List<string> names)
        {
            var filter = new BsonDocument
            {
                {
                    "name", new BsonDocument
                    {
                        { "$in", new BsonArray(names) }
                    }
                }
            };
            return await Collection.Find(filter).ToListAsync();
        }
    }
}