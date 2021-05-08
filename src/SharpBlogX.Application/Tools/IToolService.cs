using SharpBlogX.Dto.Tools.Params;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TencentCloud.Cdn.V20180606.Models;

namespace SharpBlogX.Tools
{
    public interface IToolService
    {
        Task<BlogResponse<string>> GetBingBackgroundUrlAsync();

        Task<FileContentResult> GetBingBackgroundImgAsync();

        Task<BlogResponse<List<string>>> Ip2RegionAsync(string ip);

        Task<BlogResponse> SendMessageAsync(SendMessageInput input);

        Task<FileContentResult> GetImgAsync(string url);

        Task<BlogResponse<PurgeUrlsCacheResponse>> PurgeCdnUrlsAsync(List<string> urls);

        Task<BlogResponse<PurgePathCacheResponse>> PurgeCdnPathsAsync(List<string> paths);

        Task<BlogResponse<PushUrlsCacheResponse>> PushCdnUrlsAsync(List<string> urls);
    }
}