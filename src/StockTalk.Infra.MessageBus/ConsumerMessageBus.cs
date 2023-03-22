using System.Text.Json;
using CommunityToolkit.HighPerformance;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockTalk.Infra.MessageBus.Models;

namespace StockTalk.Infra.MessageBus;

public class ConsumerMessageBus<T> : IConsumeMessageBus<T>
{
    private readonly IRabbitMqStartup _rabbitMqStartup;
    private readonly ILogger<ConsumerMessageBus<T>> _logger;
    private readonly MessageBusSettings _messageBusSettings;
    private readonly AsyncEventingBasicConsumer _eventingBasicConsumer;

    public ConsumerMessageBus(IRabbitMqStartup rabbitMqStartup,
        ILogger<ConsumerMessageBus<T>> logger,
        IOptions<MessageBusSettings> messageBusSettings)
    {
        _rabbitMqStartup = rabbitMqStartup;
        _logger = logger;
        _messageBusSettings = messageBusSettings.Value;
        _eventingBasicConsumer = new(_rabbitMqStartup.Channel);
    }
    
    public ValueTask InitializeConsumer()
    {
        _rabbitMqStartup.Channel.BasicConsume(queue: _messageBusSettings.QueueName,
            autoAck: false, consumer: _eventingBasicConsumer);

        return ValueTask.CompletedTask;
    }

    public ValueTask Handler(Action<T> actionToCall)
    {
        _eventingBasicConsumer.Received += async (sender,
            basic) =>
        {
            var payload = await JsonSerializer.DeserializeAsync<T>(basic.Body.AsStream());
            actionToCall(payload);
            _rabbitMqStartup.Channel.BasicAck(basic.DeliveryTag, false);
        };
        
        return ValueTask.CompletedTask;
    }
}