﻿using SharpBlogX.Domain.Blog;
using SharpBlogX.Dto.Blog;
using SharpBlogX.Dto.Blog.Params;
using SharpBlogX.Extensions;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpBlogX.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// Create a tag.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/tag")]
        public async Task<BlogResponse> CreateTagAsync(CreateTagInput input)
        {
            var response = new BlogResponse();

            var tag = await _tags.FindAsync(x => x.Name == input.Name);
            if (tag is not null)
            {
                response.IsFailed($"The tag:{input.Name} already exists.");
                return response;
            }

            await _tags.InsertAsync(new Tag
            {
                Name = input.Name,
                Alias = input.Alias
            });

            return response;
        }

        /// <summary>
        /// Delete tag by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/tag/{id}")]
        public async Task<BlogResponse> DeleteTagAsync(string id)
        {
            var response = new BlogResponse();

            var tag = await _tags.FindAsync(id.ToObjectId());
            if (tag is null)
            {
                response.IsFailed($"The tag id not exists.");
                return response;
            }

            await _tags.DeleteAsync(id.ToObjectId());

            return response;
        }

        /// <summary>
        /// Update tag by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/tag/{id}")]
        public async Task<BlogResponse> UpdateTagAsync(string id, UpdateTagInput input)
        {
            var response = new BlogResponse();

            var tag = await _tags.FindAsync(id.ToObjectId());
            if (tag is null)
            {
                response.IsFailed($"The tag id not exists.");
                return response;
            }

            tag.Name = input.Name;
            tag.Alias = input.Alias;

            await _tags.UpdateAsync(tag);

            return response;
        }

        /// <summary>
        /// Get the list of tags.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("api/blog/admin/tags")]
        public async Task<BlogResponse<List<GetAdminTagDto>>> GetAdminTagsAsync()
        {
            var response = new BlogResponse<List<GetAdminTagDto>>();

            var tags = await _tags.GetListAsync();

            var result = ObjectMapper.Map<List<Tag>, List<GetAdminTagDto>>(tags);
            result.ForEach(x =>
            {
                x.Total = _posts.GetCountByTagAsync(x.Id.ToObjectId()).Result;
            });

            response.Result = result;
            return response;
        }
    }
}