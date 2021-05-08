using SharpBlogX.Domain.Blog;
using SharpBlogX.Domain.Blog.Repositories;
using Volo.Abp.MongoDB;

namespace SharpBlogX.Repositories.Blog
{
    public class CategoryRepository : MongoDbRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IMongoDbContextProvider<SharpBlogXMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}