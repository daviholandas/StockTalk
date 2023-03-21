using StockTalk.Application.Models;

namespace StockTalk.Application.Services;

public interface IMessageBusService
{
        ValueTask PublishMessage(MessageStock message);
}