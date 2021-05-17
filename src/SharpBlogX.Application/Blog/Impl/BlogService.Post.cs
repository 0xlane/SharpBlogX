using SharpBlogX.Domain.Blog;
using SharpBlogX.Dto.Blog;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using System;
using System.Text;
using System.Xml;
using SharpBlogX.Extensions;

namespace SharpBlogX.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// Get post by url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Route("api/blog/post")]
        public async Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url)
        {
            return await _cache.GetPostByUrlAsync(url, async () =>
            {
                var response = new BlogResponse<PostDetailDto>();

                var post = await _posts.FindAsync(x => x.Url == url);
                if (post is null)
                {
                    response.IsFailed($"The post url not exists.");
                    return response;
                }

                var previous = _posts.Where(x => x.CreatedAt > post.CreatedAt).Take(1).Select(x => new PostPagedDto
                {
                    Title = x.Title,
                    Url = x.Url
                }).FirstOrDefault();
                var next = _posts.Where(x => x.CreatedAt < post.CreatedAt).OrderByDescending(x => x.CreatedAt).Take(1).Select(x => new PostPagedDto
                {
                    Title = x.Title,
                    Url = x.Url
                }).FirstOrDefault();

                var result = ObjectMapper.Map<Post, PostDetailDto>(post);
                result.Previous = previous;
                result.Next = next;

                response.Result = result;
                return response;
            });
        }

        /// <summary>
        /// Get the list of posts by paging.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Route("api/blog/posts/{page}/{limit}")]
        public async Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync([Range(1, 100)] int page = 1, [Range(10, 100)] int limit = 10)
        {
            return await _cache.GetPostsAsync(page, limit, async () =>
            {
                var response = new BlogResponse<PagedList<GetPostDto>>();

                var result = await _posts.GetPagedListAsync(page, limit);
                var total = result.Item1;
                var posts = GetPostList(result.Item2);

                response.Result = new PagedList<GetPostDto>(total, posts);
                return response;
            });
        }

        /// <summary>
        /// Get the list of posts by category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Route("api/blog/posts/category/{category}")]
        public async Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category)
        {
            return await _cache.GetPostsByCategoryAsync(category, async () =>
            {
                var response = new BlogResponse<List<GetPostDto>>();

                var entity = await _categories.FindAsync(x => x.Alias == category);
                if (entity is null)
                {
                    response.IsFailed($"The category:{category} not exists.");
                    return response;
                }

                var posts = await _posts.GetListByCategoryAsync(category);

                response.IsSuccess(GetPostList(posts), entity.Name);
                return response;
            });
        }

        /// <summary>
        /// Get the list of posts by tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [Route("api/blog/posts/tag/{tag}")]
        public async Task<BlogResponse<List<GetPostDto>>> GetPostsByTagAsync(string tag)
        {
            return await _cache.GetPostsByTagAsync(tag, async () =>
            {
                var response = new BlogResponse<List<GetPostDto>>();

                var entity = await _tags.FindAsync(x => x.Alias == tag);
                if (entity is null)
                {
                    response.IsFailed($"The tag:{tag} not exists.");
                    return response;
                }

                var posts = await _posts.GetListByTagAsync(tag);

                response.IsSuccess(GetPostList(posts), entity.Name);
                return response;
            });
        }

        /// <summary>
        /// Get feed of posts.
        /// </summary>
        /// <returns></returns>
        [Route("api/blog/posts/feed")]
        public async Task<BlogResponse<string>> GetRssAsync()
        {
            return await _cache.GetPostFeedAsync(async () =>
            {
                var response = new BlogResponse<string>();
                var result = await _posts.GetPagedListAsync(1, 10);
                var total = result.Item1;
                var posts = result.Item2;
                var feed = new SyndicationFeed(_blog.Value.Title, "SharpBlog", new Uri($"{_blog.Value.WebUrl}/atom.xml"), _blog.Value.WebUrl, DateTime.Now);

                if (total > 0)
                {
                    feed = new SyndicationFeed(_blog.Value.Title, "SharpBlog", new Uri($"{_blog.Value.WebUrl}/atom.xml"), _blog.Value.WebUrl, new DateTimeOffset(posts.FirstOrDefault().CreatedAt));
                }

                feed.Items = posts.Select(x =>
                {
                    var content = SyndicationContent.CreateHtmlContent(x.Markdown.ToPreviewHtml());
                    var item = new SyndicationItem()
                    {
                        Id = $"{_blog.Value.WebUrl}/post/{x.Url}",
                        Title = SyndicationContent.CreatePlaintextContent(x.Title),
                        Summary = content,
                        Content = content,
                        LastUpdatedTime = new DateTimeOffset(x.CreatedAt),
                        PublishDate = new DateTimeOffset(x.CreatedAt)
                    };
                    item.Links.Add(SyndicationLink.CreateAlternateLink(new Uri($"{_blog.Value.WebUrl}/post/{x.Url}")));
                    item.Authors.Add(new SyndicationPerson()
                    {
                        Name = x.Author
                    });
                    item.Categories.Add(new SyndicationCategory(x.Category.Name));
                    return item;
                }).ToList();

                feed.Links.Add(SyndicationLink.CreateSelfLink(new Uri(_blog.Value.WebUrl), "application/atom+xml; charset=utf-8"));
                feed.Authors.Add(new SyndicationPerson()
                {
                    Name = _blog.Value.Title
                });
                feed.Copyright = new TextSyndicationContent($"© {DateTime.Now.Year} - {_blog.Value.Title}");

                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    NewLineHandling = NewLineHandling.Entitize,
                    NewLineOnAttributes = true,
                    Indent = true
                };

                var xmlString = new StringBuilder();
                using (var xmlWriter = XmlWriter.Create(xmlString, settings))
                {
                    var rssFormatter = new Atom10FeedFormatter(feed);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                
                response.Result = xmlString.ToString();
                return response;
            });
        }

        private List<GetPostDto> GetPostList(List<Post> posts) =>
            ObjectMapper.Map<List<Post>, List<PostBriefDto>>(posts)
                        .GroupBy(x => x.Year)
                        .Select(x => new GetPostDto
                        {
                            Year = x.Key,
                            Posts = x
                        }).ToList();
    }
}