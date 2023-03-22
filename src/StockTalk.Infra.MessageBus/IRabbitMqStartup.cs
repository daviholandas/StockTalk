using RabbitMQ.Client;

namespace StockTalk.Infra.MessageBus;

public interface IRabbitMqStartup
{
    IModel Channel { get; }
}