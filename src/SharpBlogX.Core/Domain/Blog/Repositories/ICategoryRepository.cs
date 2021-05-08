using MongoDB.Bson;
using Volo.Abp.Domain.Repositories;

namespace SharpBlogX.Domain.Blog.Repositories
{
    public interface ICategoryRepository : IRepository<Category, ObjectId>
    {
    }
}