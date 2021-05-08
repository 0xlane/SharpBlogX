using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SharpBlogX.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddApplication<SharpBlogXApiModule>();

        public void Configure(IApplicationBuilder app) => app.InitializeApplication();
    }
}