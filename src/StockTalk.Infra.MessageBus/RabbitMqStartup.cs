using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StockTalk.Infra.EventBus.Services;
using StockTalk.Infra.MessageBus.Models;

namespace StockTalk.Infra.MessageBus;

public class RabbitMqStartup : BackgroundService
{
    private readonly ILogger<RabbitMqStartup> _logger;
    private readonly MessageBusSettings _messageBusSettings;
    private readonly IMessageChannelBus _messageChannelBus;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqStartup(ILogger<RabbitMqStartup> logger,
        IOptions<MessageBusSettings> messageBrokerSettings,
        IMessageChannelBus messageChannelBus)
    {
        _logger = logger;
        _messageBusSettings = messageBrokerSettings.Value;
        _messageChannelBus = messageChannelBus;
    }

    async protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Publishing messages...");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PublishMessage();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_messageBusSettings.ConnectionUri)
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _logger.LogInformation("RabbitMq broker is running....");
            
            CreateQueue(_messageBusSettings.QueueName);
            
            base.StartAsync(cancellationToken);

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();

        base.StopAsync(cancellationToken);
        _logger.LogInformation("Closing the message broker...");
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
        
        base.Dispose();
    }

    private void CreateQueue(string queueName)
        => _channel.QueueDeclare(queueName, 
            true,
            false,
            false);

    private async ValueTask PublishMessage()
    {
        await foreach (var message in _messageChannelBus.SubscribeAsync())
        {
            _channel.BasicPublish(
                exchange: "",
                routingKey: message.SentTo,
                body:Encoding.UTF8.GetBytes(message.Body));
        }
    }
}