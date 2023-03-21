namespace StockTalk.Infra.MessageBus.Models;

public class MessageBusSettings
{
    public string ConnectionUri { get; set; } = string.Empty;

    public string QueueName { get; set; } = string.Empty;
}