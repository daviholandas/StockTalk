using StockTalk.Application.Models;
using StockTalk.Infra.EventBus.Interfaces;
using StockTalk.StockWorker.Services;

namespace StockTalk.StockWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConsumeMessageBus<MessageStock> _consumeMessageBus;
    private readonly IStockService _stockService;
    private readonly IPublishMessageBus<BotMessage> _publishMessageBus;


    public Worker(ILogger<Worker> logger,
        IConsumeMessageBus<MessageStock> consumeMessageBus,
        IStockService stockService,
        IPublishMessageBus<BotMessage> publishMessageBus)
    {
        _logger = logger;
        _consumeMessageBus = consumeMessageBus;
        _stockService = stockService;
        _publishMessageBus = publishMessageBus;
    }

    public async override Task StartAsync(CancellationToken cancellationToken)
    {
        await _consumeMessageBus.InitializeConsumer();
        await base.StartAsync(cancellationToken);
    }

    async protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumeMessageBus
            .HandlerMessages(async (message) =>
            {
                var result = await _stockService.GetStockBySymbol(message?.Symbol);
                
                if(!result.IsSuccess)
                    _publishMessageBus.PublishMessage(new("Can't found the stock", message.SentTo),
                        "stocktalk.*");
                _publishMessageBus.PublishMessage(new($"{result.Value.Symbol.ToUpper()} quote is ${result.Value.Value} per share.",
                        message.SentTo),
                    "stocktalk.*");

            });
        
    }
}