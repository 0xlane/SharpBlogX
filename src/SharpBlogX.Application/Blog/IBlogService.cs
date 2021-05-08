using SharpBlogX.Response;
using System;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse<Tuple<int, int, int>>> GetStatisticsAsync();
    }
}