using SharpBlogX.Dto.Hots;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Apps
{
    public class HotModel : PageBase
    {
        public HotModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public string Id { get; set; }

        public BlogResponse<List<HotSourceDto>> HotSources { get; set; }

        public BlogResponse<HotDto> Hot { get; set; }

        public async Task OnGetAsync(string id)
        {
            HotSources = await GetResultAsync<BlogResponse<List<HotSourceDto>>>("api/hots/source");

            Id = string.IsNullOrEmpty(id) ? HotSources.Result.FirstOrDefault().Id : id;

            Hot = await GetResultAsync<BlogResponse<HotDto>>($"api/hots/{Id}");
        }
    }
}