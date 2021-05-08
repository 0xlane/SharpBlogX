using System.Collections.Generic;

namespace SharpBlogX.Dto.Messages
{
    public class MessageDto : MessageReplyDto
    {
        public List<MessageReplyDto> Reply { get; set; }
    }
}