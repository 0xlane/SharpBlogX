using AntDesign;
using SharpBlogX.Admin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace SharpBlogX.Admin.Shared
{
    public partial class Header
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public async Task OnClickCallbackAsync(MenuItem item)
        {
            switch (item.Key)
            {
                case "user":
                    NavigationManager.NavigateTo("/users");
                    break;
                case "logout":
                    {
                        var service = AuthenticationStateProvider as OAuthService;
                        await service.LogoutAsync();
                        break;
                    }
            }
        }
    }
}