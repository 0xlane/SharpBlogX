using SharpBlogX.Dto.Messages;
using SharpBlogX.Dto.Messages.Params;
using SharpBlogX.Response;
using System.Threading.Tasks;

namespace SharpBlogX.Messages
{
    public partial interface IMessageService
    {
        Task<BlogResponse> CreateAsync(CreateMessageInput input);

        Task<BlogResponse> ReplyAsync(string id, ReplyMessageInput input);

        Task<BlogResponse> DeleteAsync(string id);

        Task<BlogResponse> DeleteReplyAsync(string id, string replyId);

        Task<BlogResponse<PagedList<MessageDto>>> GetMessagesAsync(int page, int limit);
    }
}