using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Posts
{
    public class PostModel : PageBase
    {
        public PostModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public BlogResponse<PostDetailDto> Post { get; set; }

        public async Task OnGetAsync(string url)
        {
            Post = await GetResultAsync<BlogResponse<PostDetailDto>>($"api/blog/post?url={url}");
        }
    }
}