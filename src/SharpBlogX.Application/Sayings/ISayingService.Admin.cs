using SharpBlogX.Dto.Sayings;
using SharpBlogX.Dto.Sayings.Params;
using SharpBlogX.Response;
using System.Threading.Tasks;

namespace SharpBlogX.Sayings
{
    public partial interface ISayingService
    {
        Task<BlogResponse> CreateAsync(CreateInput input);

        Task<BlogResponse> DeleteAsync(string id);

        Task<BlogResponse<PagedList<SayingDto>>> GetSayingsAsync(int page, int limit);
    }
}