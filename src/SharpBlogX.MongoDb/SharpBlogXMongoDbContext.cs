using SharpBlogX.Domain.Blog;
using SharpBlogX.Domain.Hots;
using SharpBlogX.Domain.Messages;
using SharpBlogX.Domain.Sayings;
using SharpBlogX.Domain.Users;
using MongoDB.Driver;
using Volo.Abp.MongoDB;
using Tag = SharpBlogX.Domain.Blog.Tag;

namespace SharpBlogX
{
    public class SharpBlogXMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<Post> Posts => Collection<Post>();

        public IMongoCollection<Category> Categories => Collection<Category>();

        public IMongoCollection<Tag> Tags => Collection<Tag>();

        public IMongoCollection<FriendLink> FriendLinks => Collection<FriendLink>();

        public IMongoCollection<Hot> Hots => Collection<Hot>();

        public IMongoCollection<Saying> Sayings => Collection<Saying>();

        public IMongoCollection<Message> Messages => Collection<Message>();

        public IMongoCollection<User> Users => Collection<User>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Configure();
        }
    }
}