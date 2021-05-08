using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        /// <summary>
        /// Get the list of categories from the cache.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync(Func<Task<BlogResponse<List<GetCategoryDto>>>> func);
    }
}