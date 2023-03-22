using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using StockTalk.Application.Models;
using StockTalk.Application.Services;
using StockTalk.Infra.MessageBus;

namespace StockTalk.Infra.EventBus.Services;

public class MessageBusService : IMessageBusService
{
    private readonly IRabbitMqStartup _rabbitMqStartup;

    public MessageBusService(IRabbitMqStartup rabbitMqStartup)
    {
        _rabbitMqStartup = rabbitMqStartup;
    }

    public void PublishMessage(MessageStock message)
    {
        var stringfiedMessage =  JsonSerializer.Serialize(message);
        _rabbitMqStartup.Channel.BasicPublish(exchange: "",
            routingKey:"Stocks" ,
            body: Encoding.UTF8.GetBytes(stringfiedMessage));
    }
}