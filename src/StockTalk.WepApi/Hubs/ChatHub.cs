using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace StockTalk.WepApi.Hubs;

public class ChatHub : Hub
{
    private readonly IMemoryCache _memoryCache;

    public ChatHub(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async ValueTask CreateChatRoom(string id)
    {
       await Groups.AddToGroupAsync(Context.ConnectionId, id);

       _memoryCache.Set(id, id);
    }

    public async ValueTask GetAllRooms()
    {
        var names = new[] { "Stocks", "Cars", "House", "dasda" };

        await Clients.All.SendAsync("getAllGroupsListener", names);
    }
}