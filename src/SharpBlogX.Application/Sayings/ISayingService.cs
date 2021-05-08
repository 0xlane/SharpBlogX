using SharpBlogX.Response;
using System.Threading.Tasks;

namespace SharpBlogX.Sayings
{
    public partial interface ISayingService
    {
        Task<BlogResponse<string>> GetRandomAsync();
    }
}