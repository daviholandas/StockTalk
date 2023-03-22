using StockTalk.Application.Models;
using StockTalk.Application.Services;
using StockTalk.Infra.MessageBus;

namespace StockTalk.Infra.EventBus.Services;

public class MessageBusService : IMessageBusService
{

    public void PublishMessage(MessageStock message)
    {
        throw new NotImplementedException();
    }

    public ValueTask ConsumeMessages(Action<MessageStock> actionByMessage)
        => throw new NotImplementedException();
}