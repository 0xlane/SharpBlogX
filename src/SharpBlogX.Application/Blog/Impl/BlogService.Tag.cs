using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpBlogX.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// Get the list of tags.
        /// </summary>
        /// <returns></returns>
        [Route("api/blog/tags")]
        public async Task<BlogResponse<List<GetTagDto>>> GetTagsAsync()
        {
            return await _cache.GetTagsAsync(async () =>
            {
                var response = new BlogResponse<List<GetTagDto>>();

                var tags = await _tags.GetListAsync();

                var result = tags.Select(x => new GetTagDto
                {
                    Name = x.Name,
                    Alias = x.Alias,
                    Total = _posts.GetCountByTagAsync(x.Id).Result
                }).Where(x => x.Total > 0).ToList();

                response.Result = result;
                return response;
            });
        }
    }
}