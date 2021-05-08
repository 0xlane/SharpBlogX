using SharpBlogX.Dto.Blog;
using SharpBlogX.Dto.Blog.Params;
using SharpBlogX.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Blog
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreateCategoryAsync(CreateCategoryInput input);

        Task<BlogResponse> DeleteCategoryAsync(string id);

        Task<BlogResponse> UpdateCategoryAsync(string id, UpdateCategoryInput input);

        Task<BlogResponse<List<GetAdminCategoryDto>>> GetAdminCategoriesAsync();
    }
}