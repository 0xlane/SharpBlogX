using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using SharpBlogX.Extensions;
using SharpBlogX.Options;
using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace SharpBlogX.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        {
                            config.AddYamlFile("appsettings.yml", optional: true, reloadOnChange: true);

                            var configDictionary = config.Build().SerializeToJson();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"appsettings {@configDictionary}");
                            Console.ResetColor();
                        })
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        })
                        .ConfigureServices((hostingContext, services) =>
                        {
                            var blog = new BlogOptions();
                            services.Configure<BlogOptions>( options =>
                            {
                                var blogOption = hostingContext.Configuration.GetSection("blog");
                                options.AdminUrl = blogOption.GetValue<string>(nameof(BlogOptions.AdminUrl));
                                options.ApiUrl = blogOption.GetValue<string>(nameof(BlogOptions.ApiUrl));
                                options.WebUrl = blogOption.GetValue<string>(nameof(BlogOptions.WebUrl));
                                options.StaticUrl = blogOption.GetValue<string>(nameof(BlogOptions.StaticUrl));
                                options.TelegramUrl = blogOption.GetValue<string>(nameof(BlogOptions.TelegramUrl));
                                options.GithubUrl = blogOption.GetValue<string>(nameof(BlogOptions.GithubUrl));
                                options.Title = blogOption.GetValue<string>(nameof(BlogOptions.Title));
                                
                                blog = options;
                            });
                            services.AddRazorPages();
                            services.AddServerSideBlazor()
                                    .AddHubOptions(options =>
                                    {
                                        options.EnableDetailedErrors = true;
                                        options.MaximumReceiveMessageSize = 10 * 1024 * 1024;
                                    });
                            services.Configure<WebEncoderOptions>(options =>
                            {
                                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
                            });
                            services.AddHttpClient("api", x =>
                            {
                                x.BaseAddress = new Uri(blog.ApiUrl);
                            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler{
                                ServerCertificateCustomValidationCallback =
                                (httpRequestMessage, cert, cetChain, policyErrors) =>
                                {
                                    return true;
                            }});
                        });
            await host.Build().RunAsync();
        }
    }
}