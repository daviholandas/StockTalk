using StockTalk.Application.Models;
using StockTalk.Infra.EventBus.Interfaces;
using StockTalk.StockWorker.Services;

namespace StockTalk.StockWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConsumeMessageBus<MessageStock> _consumeMessageBus;
    private readonly IStockService _stockService;


    public Worker(ILogger<Worker> logger,
        IConsumeMessageBus<MessageStock> consumeMessageBus,
        IStockService stockService)
    {
        _logger = logger;
        _consumeMessageBus = consumeMessageBus;
        _stockService = stockService;
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
                _logger.LogInformation($"{result.Value.Symbol.ToUpper()} quote is ${result.Value.Value} per share");
                
            });

        await Task.CompletedTask;
    }
}