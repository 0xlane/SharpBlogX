using SharpBlogX.Domain.Blog;
using SharpBlogX.Domain.Blog.Repositories;
using Volo.Abp.MongoDB;

namespace SharpBlogX.Repositories.Blog
{
    public class FriendLinkRepository : MongoDbRepositoryBase<FriendLink>, IFriendLinkRepository
    {
        public FriendLinkRepository(IMongoDbContextProvider<SharpBlogXMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}