using SharpBlogX.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;

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

            context.Services.AddHttpClient();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}