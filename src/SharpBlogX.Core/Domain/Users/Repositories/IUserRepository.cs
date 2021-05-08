using MongoDB.Bson;
using Volo.Abp.Domain.Repositories;

namespace SharpBlogX.Domain.Users.Repositories
{
    public interface IUserRepository : IRepository<User, ObjectId>
    {
    }
}