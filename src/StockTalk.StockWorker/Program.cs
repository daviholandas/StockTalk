using StockTalk.StockWorker;
using StockTalk.StockWorker.Models;
using StockTalk.StockWorker.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var applicationSettings = context
            .Configuration
            .GetSection(nameof(ApplicationSettings))
            .Get<ApplicationSettings>();

        services
            .Configure<ApplicationSettings>(context
                .Configuration
                .GetSection(nameof(applicationSettings)));

        services.AddHttpClient(applicationSettings.ClientSettings.Name,
            client =>
        {
            client.BaseAddress = new Uri(applicationSettings.ClientSettings.BaseUrl);
        });
        
        services.AddTransient<IStockService, StockService>();
        
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();