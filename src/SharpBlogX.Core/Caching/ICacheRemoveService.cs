using System.Threading.Tasks;

namespace SharpBlogX.Caching
{
    public interface ICacheRemoveService
    {
        Task RemoveAsync(string key);
    }
}