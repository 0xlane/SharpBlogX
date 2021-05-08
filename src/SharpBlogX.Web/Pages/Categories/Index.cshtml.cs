using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Web.Pages.Categories
{
    public class IndexModel : PageBase
    {
        public IndexModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public BlogResponse<List<GetCategoryDto>> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await GetResultAsync<BlogResponse<List<GetCategoryDto>>>("api/blog/categories");
        }
    }
}