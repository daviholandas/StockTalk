using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using StockTalk.Application.Aggregates.ChatAggregate;
using StockTalk.Application.Models;
using StockTalk.Infra.EventBus.Interfaces;

namespace StockTalk.WepApi.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly IMemoryCache _memoryCache;
    private readonly IPublishMessageBus<MessageStock> _publishMessageBus;

    public ChatHub(IMemoryCache memoryCache,
        IPublishMessageBus<MessageStock> publishMessageBus)
    {
        _memoryCache = memoryCache;
        _publishMessageBus = publishMessageBus;
    }

    public async ValueTask CreateChatRoom(string groupName)
        => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    public async ValueTask SendMessages(string groupName, Message message )
    {
        await Clients.Group(groupName).ReceiveMessage(message);
    }

    public void GetStock(MessageStock messageStock)
        => _publishMessageBus.PublishMessage(messageStock, "stockbot.*");
}