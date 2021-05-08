using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse<List<GetTagDto>>> GetTagsAsync();
    }
}