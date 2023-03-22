using StockTalk.Infra.MessageBus.IoC;
using StockTalk.StockWorker;
using StockTalk.StockWorker.Models;
using StockTalk.StockWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configurationSection = context.Configuration.GetSection(nameof(WorkerSettings));

        var workSettings = configurationSection.Get<WorkerSettings>();

        services
            .Configure<WorkerSettings>(configurationSection);
        
        services.AddHttpClient<IStockService>(workSettings.ClientSettings.Name,
            client =>
        {
            client.BaseAddress = new Uri(workSettings.ClientSettings.BaseUrl);
        });

        services.AddTransient<IStockService, StockService>();

        services.AddRabbitMqBroker(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();