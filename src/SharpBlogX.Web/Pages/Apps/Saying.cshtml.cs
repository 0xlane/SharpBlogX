using SharpBlogX.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Apps
{
    public class SayingModel : PageBase
    {
        public SayingModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public string Saying { get; set; }

        public async Task OnGetAsync()
        {
            var response = await GetResultAsync<BlogResponse<string>>("api/saying/random");
            Saying = response.Result;
        }
    }
}