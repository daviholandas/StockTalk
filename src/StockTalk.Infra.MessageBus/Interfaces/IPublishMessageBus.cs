namespace StockTalk.Infra.EventBus.Interfaces;

public interface IPublishMessageBus<in T>
{
    void PublishMessage(T message);
}