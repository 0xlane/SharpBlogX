using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.FriendLinks
{
    public class IndexModel : PageBase
    {
        public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public BlogResponse<List<FriendLinkDto>> FriendLinks { get; set; }

        public async Task OnGetAsync()
        {
            FriendLinks = await GetResultAsync<BlogResponse<List<FriendLinkDto>>>("/api/blog/friendlinks");
        }
    }
}