using StockTalk.Application.Models;
using StockTalk.Application.Services;

namespace StockTalk.Infra.EventBus.Services;

public class MessageBusService : IMessageBusService
{
    private IMessageChannelBus _messageChannelBus;

    public MessageBusService(IMessageChannelBus messageChannelBus)
    {
        _messageChannelBus = messageChannelBus;
    }

    public async ValueTask PublishMessage(MessageStock message)
        => await _messageChannelBus.PublishMessageAsync(message);
}