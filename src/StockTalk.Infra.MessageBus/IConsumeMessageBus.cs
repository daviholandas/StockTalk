using RabbitMQ.Client.Events;

namespace StockTalk.Infra.MessageBus;

public interface IConsumeMessageBus<out T>
{
    ValueTask InitializeConsumer();

    ValueTask Handler(Action<T> actionToCall);
}