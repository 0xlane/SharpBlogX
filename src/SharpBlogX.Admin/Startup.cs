using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using SharpBlogX.Options;

namespace SharpBlogX.Admin
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<BlogOptions> blog)
        {
            app.UseForwardedHeaders();
            app.UsePathBase(new System.Uri(blog.Value.AdminUrl).PathAndQuery);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            
            app.UseSerilogRequestLogging();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}