using SharpBlogX.Domain.Users;
using SharpBlogX.Domain.Users.Repositories;
using Volo.Abp.MongoDB;

namespace SharpBlogX.Repositories.Users
{
    public class UserRepository : MongoDbRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoDbContextProvider<SharpBlogXMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}