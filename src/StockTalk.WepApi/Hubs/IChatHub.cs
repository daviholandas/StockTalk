using StockTalk.Application.Aggregates.ChatAggregate;

namespace StockTalk.WepApi.Hubs;

public interface IChatHub
{
    Task ReceiveMessage(Message message);
}