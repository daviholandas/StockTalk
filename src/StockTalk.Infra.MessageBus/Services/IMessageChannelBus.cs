using System.Threading.Channels;
using StockTalk.Application.Models;

namespace StockTalk.Infra.EventBus.Services;

public interface IMessageChannelBus
{
    ValueTask PublishMessageAsync(MessageStock message);

    IAsyncEnumerable<MessageStock> SubscribeAsync();
}