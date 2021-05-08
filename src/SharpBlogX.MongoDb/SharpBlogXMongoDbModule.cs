using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace SharpBlogX
{
    [DependsOn(
        typeof(AbpAuditLoggingMongoDbModule),
        typeof(SharpBlogXCoreModule)
    )]
    public class SharpBlogXMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<SharpBlogXMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });

            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabledForGetRequests = true;
            });

            AbpAuditLoggingDbProperties.DbTablePrefix = "sharpblog_";

            ConventionRegistry.Register("CamelCase", new ConventionPack { new CamelCaseElementNameConvention() }, type => true);
        }
    }
}