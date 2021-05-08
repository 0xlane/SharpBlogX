using SharpBlogX.Dto.Blog;
using SharpBlogX.Dto.Blog.Params;
using SharpBlogX.Response;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreatePostAsync(CreatePostInput input);

        Task<BlogResponse> DeletePostAsync(string id);

        Task<BlogResponse> UpdatePostAsync(string id, UpdatePostInput input);

        Task<BlogResponse<PostDto>> GetPostAsync(string id);

        Task<BlogResponse<PagedList<GetAdminPostDto>>> GetAdminPostsAsync(int page, int limit);
    }
}