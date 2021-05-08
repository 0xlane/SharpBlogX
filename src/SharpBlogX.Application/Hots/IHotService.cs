using SharpBlogX.Dto.Hots;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Hots
{
    public interface IHotService
    {
        Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync();

        Task<BlogResponse<HotDto>> GetHotsAsync(string id);
    }
}