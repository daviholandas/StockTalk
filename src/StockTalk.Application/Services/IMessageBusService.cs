using StockTalk.Application.Models;

namespace StockTalk.Application.Services;

public interface IMessageBusService
{ 
    void PublishMessage(MessageStock message);
    
    ValueTask ConsumeMessages(Action<MessageStock> actionByMessage);
}