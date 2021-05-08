using MongoDB.Bson;
using Volo.Abp.Domain.Entities;

namespace SharpBlogX.Domain
{
    public abstract class EntityBase : Entity<ObjectId>
    {
    }
}