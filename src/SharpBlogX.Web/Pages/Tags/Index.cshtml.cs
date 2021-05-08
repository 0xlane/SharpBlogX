using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Tags
{
    public class IndexModel : PageBase
    {
        public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public BlogResponse<List<GetTagDto>> Tags { get; set; }

        public async Task OnGetAsync()
        {
            Tags = await GetResultAsync<BlogResponse<List<GetTagDto>>>("api/blog/tags");
        }
    }
}