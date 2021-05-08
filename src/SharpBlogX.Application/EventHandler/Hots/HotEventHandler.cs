using SharpBlogX.Caching;
using SharpBlogX.Caching.Hots;
using SharpBlogX.EventData.Hots;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace SharpBlogX.EventHandler.Hots
{
    public class HotEventHandler : ILocalEventHandler<HotWorkerEventData>, ITransientDependency
    {
        private readonly IHotCacheService _cache;

        public HotEventHandler(IHotCacheService cache)
        {
            _cache = cache;
        }

        public async Task HandleEventAsync(HotWorkerEventData eventData)
        {
            await _cache.RemoveAsync(CachingConsts.CachePrefix.Hot);
        }
    }
}