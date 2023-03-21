using System.Threading.Channels;
using StockTalk.Application.Models;

namespace StockTalk.Infra.EventBus.Services;

internal class MessageChannelBus : IMessageChannelBus
{
    private static Channel<MessageStock> _channel
        => Channel.CreateUnbounded<MessageStock>(new()
        {
            SingleReader = true,
            SingleWriter = false
        });

    public async ValueTask PublishMessageAsync(MessageStock message)
        => await _channel.Writer.WriteAsync(message, CancellationToken.None)
            .ConfigureAwait(false);

    public async IAsyncEnumerable<MessageStock> SubscribeAsync()
    {
        await foreach (var message in _channel.Reader.ReadAllAsync(CancellationToken.None))
            yield return message;
    }
}