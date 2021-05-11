using SharpBlogX.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using System.Net.Http;
using System.Net;

namespace SharpBlogX
{
    [DependsOn(
        typeof(AbpBackgroundWorkersQuartzModule),
        typeof(SharpBlogXCoreModule)
    )]
    public class SharpBlogXBackgroundWorkersModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var option = context.Services.ExecutePreConfiguredActions<WorkerOptions>();

            Configure<AbpBackgroundWorkerQuartzOptions>(options =>
            {
                options.IsAutoRegisterEnabled = option.IsEnabled;
            });

            context.Services.AddHttpClient("hot")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                });

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}