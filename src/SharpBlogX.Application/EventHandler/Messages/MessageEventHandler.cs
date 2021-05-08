using SharpBlogX.Domain.Messages;
using SharpBlogX.Dto.Tools.Params;
using SharpBlogX.Tools;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace SharpBlogX.EventHandler.Messages
{
    public class MessageEventHandler : ILocalEventHandler<EntityCreatedEventData<Message>>,
                                       ITransientDependency
    {
        private readonly IToolService _toolService;

        public MessageEventHandler(IToolService toolService)
        {
            _toolService = toolService;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<Message> eventData)
        {
            await _toolService.SendMessageAsync(new SendMessageInput
            {
                Text = $"消息来自：{eventData.Entity.Name}",
                Desc = eventData.Entity.Content
            });
        }
    }
}