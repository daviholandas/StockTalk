using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StockTalk.Infra.MessageBus.Models;

namespace StockTalk.Infra.MessageBus;

public class RabbitMqStartup : IRabbitMqStartup, IDisposable
{
    private readonly ILogger<RabbitMqStartup> _logger;
    private readonly MessageBusSettings _messageBusSettings;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public IModel Channel
        => _channel;

    public RabbitMqStartup(ILogger<RabbitMqStartup> logger,
        IOptions<MessageBusSettings> messageBusSettings)
    {
        _logger = logger;
        _messageBusSettings = messageBusSettings.Value;
        
        try
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_messageBusSettings.ConnectionUri),
                DispatchConsumersAsync = true
            };
        
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_messageBusSettings.QueueName,
                true, false, false);
            
            _logger.LogInformation("Success to connect a RabbitMq Broker....");
        }
        catch (Exception e)
        {
            _logger.LogError(e, 
                "Can't connect to RabbitMq broker...", 
                e.Message);
            throw;
        }
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();

        GC.SuppressFinalize(this);
    }
}