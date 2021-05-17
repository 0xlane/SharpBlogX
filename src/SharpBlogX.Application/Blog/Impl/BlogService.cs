using SharpBlogX.Caching.Blog;
using SharpBlogX.Domain.Blog.Repositories;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SharpBlogX.Options;

namespace SharpBlogX.Blog.Impl
{
    public partial class BlogService : ServiceBase, IBlogService
    {
        private readonly IPostRepository _posts;
        private readonly ICategoryRepository _categories;
        private readonly ITagRepository _tags;
        private readonly IFriendLinkRepository _friendLinks;
        private readonly IBlogCacheService _cache;
        private readonly IOptions<BlogOptions> _blog;

        public BlogService(IPostRepository posts,
                           ICategoryRepository categories,
                           ITagRepository tags,
                           IFriendLinkRepository friendLinks,
                           IBlogCacheService cache,
                           IOptions<BlogOptions> blog)
        {
            _posts = posts;
            _categories = categories;
            _tags = tags;
            _friendLinks = friendLinks;
            _cache = cache;
            _blog = blog;
        }

        /// <summary>
        /// Get statistics on the total number of posts, categories and tags.
        /// </summary>
        /// <returns></returns>
        [Route("api/blog/statistics")]
        public async Task<BlogResponse<Tuple<int, int, int>>> GetStatisticsAsync()
        {
            var response = new BlogResponse<Tuple<int, int, int>>();

            var postCount = await _posts.GetCountAsync();
            var categoryCount = await _categories.GetCountAsync();
            var tagCount = await _tags.GetCountAsync();

            response.Result = new Tuple<int, int, int>(postCount.To<int>(), categoryCount.To<int>(), tagCount.To<int>());
            return response;
        }
    }
}