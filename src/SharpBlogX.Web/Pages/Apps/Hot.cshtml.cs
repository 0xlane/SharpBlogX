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

        public string Source { get; set; }

        public BlogResponse<List<HotSourceDto>> HotSources { get; set; }

        public BlogResponse<HotDto> Hot { get; set; }

        public async Task OnGetAsync(string source)
        {
            HotSources = await GetResultAsync<BlogResponse<List<HotSourceDto>>>("api/hots/source");

            HotSources.Result.Sort((x, y) => x.Source.CompareTo(y.Source));

            Source = string.IsNullOrEmpty(source) ? HotSources.Result.FirstOrDefault().Source : source;

            Hot = await GetResultAsync<BlogResponse<HotDto>>($"api/hots/{Source}");
        }
    }
}