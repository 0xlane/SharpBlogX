using SharpBlogX.Domain.Users;
using SharpBlogX.Dto.Users;
using SharpBlogX.Dto.Users.Params;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Users
{
    public interface IUserService
    {
        Task<BlogResponse> CreateUserAsync(CreateUserInput input);

        Task<BlogResponse> DeleteUserAsync(string id);

        Task<BlogResponse> UpdateUserAsync(string id, UpdateUserinput input);

        Task<BlogResponse> UpdatePasswordAsync(string id, string password);

        Task<BlogResponse> SettingAdminAsync(string id, bool isAdmin);

        Task<BlogResponse<List<UserDto>>> GetUsersAsync();

        Task<BlogResponse<UserDto>> GetUserAsync(string id);

        Task<BlogResponse<UserDto>> GetCurrentUserAsync();

        Task<User> CreateUserAsync(string username, string type, string identity, string name, string avatar, string email);

        Task<User> VerifyByAccountAsync(string username, string password);

        Task<User> GetDefaultUserAsync();
    }
}