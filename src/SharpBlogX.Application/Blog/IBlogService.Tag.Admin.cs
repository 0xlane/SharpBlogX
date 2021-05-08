using SharpBlogX.Dto.Blog;
using SharpBlogX.Dto.Blog.Params;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreateTagAsync(CreateTagInput input);

        Task<BlogResponse> DeleteTagAsync(string id);

        Task<BlogResponse> UpdateTagAsync(string id, UpdateTagInput input);

        Task<BlogResponse<List<GetAdminTagDto>>> GetAdminTagsAsync();
    }
}