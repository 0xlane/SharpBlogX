using AntDesign.Pro.Layout;
using SharpBlogX.Admin.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using SharpBlogX.Extensions;
using SharpBlogX.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using System.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Authentication;
using Serilog;
using Serilog.Events;

namespace SharpBlogX.Admin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

            try
            {
                Log.Information("Application Starting.");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
                Log.Information("Application has closed!");
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
                    var https = new HttpsOptions();
                    var blog = new BlogOptions();

                    var httpsOption = hostingContext.Configuration.GetSection("https");
                    https.ListenAddress = httpsOption.GetValue<string>(nameof(https.ListenAddress));
                    https.ListenPort = httpsOption.GetValue<int>(nameof(https.ListenPort));
                    https.PublicCertFile = httpsOption.GetValue<string>(nameof(https.PublicCertFile));
                    https.PrivateCertFile = httpsOption.GetValue<string>(nameof(https.PrivateCertFile));
                    services.Configure<HttpsOptions>(httpsOption);

                    var blogOption = hostingContext.Configuration.GetSection("blog");
                    blog.AdminUrl = blogOption.GetValue<string>(nameof(BlogOptions.AdminUrl));
                    blog.ApiUrl = blogOption.GetValue<string>(nameof(BlogOptions.ApiUrl));
                    blog.WebUrl = blogOption.GetValue<string>(nameof(BlogOptions.WebUrl));
                    blog.StaticUrl = blogOption.GetValue<string>(nameof(BlogOptions.StaticUrl));
                    blog.TelegramUrl = blogOption.GetValue<string>(nameof(BlogOptions.TelegramUrl));
                    blog.GithubUrl = blogOption.GetValue<string>(nameof(BlogOptions.GithubUrl));
                    blog.Title = blogOption.GetValue<string>(nameof(BlogOptions.Title));
                    services.Configure<BlogOptions>(blogOption);

                    services.Configure<KestrelServerOptions>(options => 
                    {
                        IPAddress address = null;
                        try
                        {
                            address = IPAddress.Parse(https.ListenAddress);
                        }
                        catch (FormatException)
                        {
                            address = Dns.GetHostAddresses(https.ListenAddress).FirstOrDefault();
                        }
                        IPEndPoint SharpBlogEndpoint = new IPEndPoint(address, https.ListenPort);

                        options.Listen(SharpBlogEndpoint, listenOptions => 
                        {
                            listenOptions.UseHttps(httpsOptions =>
                            {
                                if (!File.Exists(https.PrivateCertFile) || !File.Exists(https.PublicCertFile))
                                {
                                    X509Certificate2 certificate = SharpBlogEndpoint.Address.CreateSelfSignedCertificate("CN=SharpBlog");
                                    File.WriteAllBytes(https.PrivateCertFile, certificate.Export(X509ContentType.Pfx));
                                    File.WriteAllBytes(https.PublicCertFile, certificate.Export(X509ContentType.Cert));
                                }
                                try
                                {
                                    httpsOptions.ServerCertificate = new X509Certificate2(https.PrivateCertFile);
                                }
                                catch (CryptographicException)
                                {
                                    Console.Error.WriteLine("Error importing SharpBlog certificate.");
                                }
                                httpsOptions.SslProtocols = SslProtocols.Tls12;
                            });
                        });
                    });

                    services.AddRazorPages();
                    services.AddServerSideBlazor()
                            .AddHubOptions(options =>
                            {
                                options.EnableDetailedErrors = true;
                                options.MaximumReceiveMessageSize = 10 * 1024 * 1024;
                            });
                    services.AddAntDesign();
                    services.AddScoped<AuthenticationStateProvider, OAuthService>();
                    services.Configure<ProSettings>(x =>
                    {
                        x.Title = blog.Title;
                        x.NavTheme = "light";
                        x.Layout = "mix";
                        x.PrimaryColor = "daybreak";
                        x.ContentWidth = "Fluid";
                        x.HeaderHeight = 50;
                    });
                    services.AddHttpClient("api", x =>
                    {
                        x.BaseAddress = new Uri(blog.ApiUrl);
                    }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler{
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => {
                            return true;
                    }});
                }).UseSerilog();
    }
}