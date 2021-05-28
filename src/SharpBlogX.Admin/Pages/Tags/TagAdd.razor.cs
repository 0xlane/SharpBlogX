using SharpBlogX.Dto.Blog.Params;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SharpBlogX.Admin.Pages.Tags
{
    public partial class TagAdd
    {
        CreateTagInput input = new CreateTagInput();

        public async Task HandleSubmit()
        {
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Alias))
            {
                return;
            }

            var json = JsonConvert.SerializeObject(input);

            var response = await GetResultAsync<BlogResponse>("api/blog/tag", json, HttpMethod.Post);
            if (response.Success)
            {
                await Message.Success("Successful", 0.5);
                NavigationManager.NavigateTo("tags/list");
            }
            else
            {
                await Message.Error(response.Message);
            }
        }
    }
}