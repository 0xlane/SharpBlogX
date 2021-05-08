using SharpBlogX.Domain.Sayings.Repositories;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SharpBlogX.Sayings.Impl
{
    public partial class SayingService : ServiceBase, ISayingService
    {
        private readonly ISayingRepository _sayings;

        public SayingService(ISayingRepository sayings)
        {
            _sayings = sayings;
        }

        /// <summary>
        /// Get a saying.
        /// </summary>
        /// <returns></returns>
        [Route("api/saying/random")]
        public async Task<BlogResponse<string>> GetRandomAsync()
        {
            var response = new BlogResponse<string>();

            var saying = await _sayings.GetRandomAsync();

            response.Result = saying.Content;
            return response;
        }
    }
}