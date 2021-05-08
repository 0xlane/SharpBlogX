using SharpBlogX.Domain.Blog;
using SharpBlogX.Domain.Hots;
using SharpBlogX.Domain.Messages;
using SharpBlogX.Domain.Sayings;
using SharpBlogX.Domain.Users;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace SharpBlogX
{
    public static class SharpBlogXDbContextModelCreatingExtensions
    {
        public static void Configure(this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Post>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.Post;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Category>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.Category;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Tag>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.Tag;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<FriendLink>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.FriendLink;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Hot>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.Hot;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Saying>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.Saying;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<Message>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.Message;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });

            builder.Entity<User>(b =>
            {
                b.CollectionName = SharpBlogXDbConsts.CollectionNames.User;
                b.BsonMap.AutoMap();
                b.BsonMap.SetIgnoreExtraElements(true);
            });
        }
    }
}