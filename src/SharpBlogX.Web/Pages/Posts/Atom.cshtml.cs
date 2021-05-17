using System.Net.Http;
using System.Threading.Tasks;
using SharpBlogX.Response;

namespace SharpBlogX.Web.Pages.Posts
{
    public class AtomModel : PageBase
    {
        public AtomModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public BlogResponse<string> Feed { get; set; }

        public async Task OnGetAsync(string category)
        {
            Feed = await GetResultAsync<BlogResponse<string>>("api/blog/posts/feed");
        }
    }
}