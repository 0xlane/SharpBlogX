using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url);

        Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync(int page, int limit);

        Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category);

        Task<BlogResponse<List<GetPostDto>>> GetPostsByTagAsync(string tag);
    }
}