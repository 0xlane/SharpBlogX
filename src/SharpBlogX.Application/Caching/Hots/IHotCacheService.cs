﻿using SharpBlogX.Dto.Hots;
using SharpBlogX.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Caching.Hots
{
    public interface IHotCacheService : ICacheRemoveService
    {
        /// <summary>
        /// Get the list of sources from the cache.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync(Func<Task<BlogResponse<List<HotSourceDto>>>> func);

        /// <summary>
        /// Get the list of hot news by id from the cache.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<HotDto>> GetHotsAsync(string id, Func<Task<BlogResponse<HotDto>>> func);

        /// <summary>
        /// Get the list of hot news by source name from the cache.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<HotDto>> GetHotsBySourceAsync(string source, Func<Task<BlogResponse<HotDto>>> func);
    }
}