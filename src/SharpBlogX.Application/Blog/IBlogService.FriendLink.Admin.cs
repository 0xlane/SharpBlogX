using SharpBlogX.Dto.Blog;
using SharpBlogX.Dto.Blog.Params;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreateFriendLinkAsync(CreateFriendLinkInput input);

        Task<BlogResponse> DeleteFriendLinkAsync(string id);

        Task<BlogResponse> UpdateFriendLinkAsync(string id, UpdateFriendLinkInput input);

        Task<BlogResponse<List<GetAdminFriendLinkDto>>> GetAdminFriendLinksAsync();
    }
}