using System.Threading.Tasks;

namespace SharpBlogX.Caching.Authorize
{
    public interface IAuthorizeCacheService
    {
        Task AddAuthorizeCodeAsync(string code);

        Task<string> GetAuthorizeCodeAsync();
    }
}