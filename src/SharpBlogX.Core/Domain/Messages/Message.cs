using System.Collections.Generic;

namespace SharpBlogX.Domain.Messages
{
    public class Message : MessageReply
    {
        public List<MessageReply> Reply { get; set; }
    }
}