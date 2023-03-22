using Microsoft.AspNetCore.SignalR;
using StockTalk.Application.Aggregates.ChatAggregate;
using StockTalk.Application.Models;
using StockTalk.Infra.EventBus.Interfaces;
using StockTalk.WepApi.Hubs;

namespace StockTalk.WepApi;

public class MessageBusConsumer : BackgroundService
{
    private readonly IConsumeMessageBus<BotMessage> _consumeMessageBus;
    private readonly ILogger<MessageBusConsumer> _logger;
    private readonly IHubContext<ChatHub,IChatHub> _chatHubContext;

    public MessageBusConsumer(IConsumeMessageBus<BotMessage> consumeMessageBus,
        ILogger<MessageBusConsumer> logger,
        IHubContext<ChatHub, IChatHub> chatHubContext)
    {
        _consumeMessageBus = consumeMessageBus;
        _logger = logger;
        _chatHubContext = chatHubContext;
    }

    public async override Task StartAsync(CancellationToken cancellationToken)
    {
        await _consumeMessageBus.InitializeConsumer();
        await base.StartAsync(cancellationToken);
    }


    async protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumeMessageBus.HandlerMessages(stock =>
        {
            _logger.LogInformation(stock.ToString());
            _chatHubContext.Clients.Group(stock.SendTo)
                .ReceiveMessage(new Message(Body:stock.Body, SentAt: DateTime.Now.ToString(), SentTo:"StockBot"));
        });
        await Task.CompletedTask;
    }
}