using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Posts
{
    public class IndexModel : PageBase
    {
        public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public int PageIndex { get; set; }

        public override int PageSize { get; set; } = 15;

        public BlogResponse<PagedList<GetPostDto>> Posts { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            PageIndex = pageIndex;

            Posts = await GetResultAsync<BlogResponse<PagedList<GetPostDto>>>($"api/blog/posts/{PageIndex}/{PageSize}");
        }
    }
}