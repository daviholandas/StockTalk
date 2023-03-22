using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StockTalk.Infra.EventBus.Interfaces;
using StockTalk.Infra.MessageBus.Models;

namespace StockTalk.Infra.MessageBus;

public class PublishMessageBus<T> : IPublishMessageBus<T>
{
    private readonly IRabbitMqStartup _rabbitMqStartup;
    private readonly MessageBusSettings _messageBusSettings;

    public PublishMessageBus(IRabbitMqStartup rabbitMqStartup,
        IOptions<MessageBusSettings> messageBusSettings)
    {
        _rabbitMqStartup = rabbitMqStartup;
        _messageBusSettings = messageBusSettings.Value;
    }

    public void PublishMessage(T message)
    {
        var stringfiedMessage =  JsonSerializer.Serialize(message);
        _rabbitMqStartup.Channel.BasicPublish(exchange: "",
            routingKey:_messageBusSettings.QueueName,
            body: Encoding.UTF8.GetBytes(stringfiedMessage)); 
    }
}