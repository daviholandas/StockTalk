using RabbitMQ.Client;

namespace StockTalk.Infra.EventBus.Interfaces;

public interface IRabbitMqStartup
{
    IModel Channel { get; }
}