using SharpBlogX.Dto.Authorize.Params;
using SharpBlogX.Response;
using SharpBlogX.Users;
using System.Threading.Tasks;

namespace SharpBlogX.Authorize
{
    public interface IAuthorizeService
    {
        Task<BlogResponse<string>> GetAuthorizeUrlAsync(string type);

        Task<BlogResponse<string>> GenerateTokenAsync(string type, string code, string state);

        Task<BlogResponse<string>> GenerateTokenAsync(string code);

        Task<BlogResponse<string>> GenerateTokenAsync(IUserService userService, AccountInput input);

        Task<BlogResponse> SendAuthorizeCodeAsync();
    }
}