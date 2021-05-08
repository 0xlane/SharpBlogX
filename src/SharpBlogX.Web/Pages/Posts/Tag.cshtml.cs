using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Posts
{
    public class TagModel : PageBase
    {
        public TagModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public BlogResponse<List<GetPostDto>> Posts { get; set; }

        public async Task OnGetAsync(string tag)
        {
            Posts = await GetResultAsync<BlogResponse<List<GetPostDto>>>($"api/blog/posts/tag/{tag}");
        }
    }
}