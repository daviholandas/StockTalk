using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTalk.Application.Services;
using StockTalk.Infra.EventBus.Services;
using StockTalk.Infra.MessageBus.Models;

namespace StockTalk.Infra.MessageBus.IoC;

public static class MessageBrokerIoC
{
    public static IServiceCollection AddRabbitMqBroker(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .Configure<MessageBusSettings>(configuration.GetSection(nameof(MessageBusSettings)));

        serviceCollection.AddSingleton<IMessageChannelBus, MessageChannelBus>();
        serviceCollection.AddSingleton<IMessageBusService, MessageBusService>();

        serviceCollection.AddHostedService<RabbitMqStartup>();
        return serviceCollection;
    }
}