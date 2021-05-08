using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace SharpBlogX
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(SharpBlogXCoreModule)
    )]
    public class SharpBlogXApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<SharpBlogXApplicationModule>();
                options.AddProfile<SharpBlogXApplicationAutoMapperProfile>();
            });
        }
    }
}