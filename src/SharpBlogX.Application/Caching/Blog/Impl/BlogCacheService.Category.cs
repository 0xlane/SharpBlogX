using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Caching.Blog.Impl
{
    public partial class BlogCacheService
    {
        public async Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync(Func<Task<BlogResponse<List<GetCategoryDto>>>> func) => await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetCategories(), func, CachingConsts.CacheStrategy.HALF_DAY);
    }
}