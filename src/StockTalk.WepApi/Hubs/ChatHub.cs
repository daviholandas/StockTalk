using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using StockTalk.Application.Aggregates.ChatAggregate;

namespace StockTalk.WepApi.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly IMemoryCache _memoryCache;

    public ChatHub(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async ValueTask CreateChatRoom(string groupName)
        => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

    public async ValueTask SendMessages(string groupName, Message message )
    {
        await Clients.Group(groupName).ReceiveMessage(message);
    }
}