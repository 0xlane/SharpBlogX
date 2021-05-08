using SharpBlogX.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace SharpBlogX.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SharpBlogXApplicationModule),
        typeof(SharpBlogXMongoDbModule)
    )]
    public class SharpBlogXDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddYamlFile("appsettings.yml", true, true)
                                                   .Build();

            context.Services.Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = config.GetSection("storage").GetValue<string>("mongodb");
            });
        }
    }
}