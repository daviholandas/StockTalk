namespace StockTalk.Infra.EventBus.Interfaces;

public interface IConsumeMessageBus<out T>
{
    ValueTask InitializeConsumer();

    ValueTask HandlerMessages(Action<T> actionToCall);
}