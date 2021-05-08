using MongoDB.Bson;
using Volo.Abp.Domain.Repositories;

namespace SharpBlogX.Domain.Blog.Repositories
{
    public interface IFriendLinkRepository : IRepository<FriendLink, ObjectId>
    {
    }
}