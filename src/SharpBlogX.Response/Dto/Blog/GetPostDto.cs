using System.Collections.Generic;

namespace SharpBlogX.Dto.Blog
{
    public class GetPostDto
    {
        public int Year { get; set; }

        public IEnumerable<PostBriefDto> Posts { get; set; }
    }
}