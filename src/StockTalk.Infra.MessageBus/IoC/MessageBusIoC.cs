using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTalk.Application.Models;
using StockTalk.Application.Services;
using StockTalk.Infra.EventBus.Interfaces;
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
        
        serviceCollection.AddSingleton<IConsumeMessageBus<MessageStock>, ConsumerMessageBus<MessageStock>>();
        serviceCollection.AddSingleton<IPublishMessageBus<MessageStock>, PublishMessageBus<MessageStock>>();
        serviceCollection.AddSingleton<IConsumeMessageBus<BotMessage>, ConsumerMessageBus<BotMessage>>();
        serviceCollection.AddSingleton<IPublishMessageBus<BotMessage>, PublishMessageBus<BotMessage>>();
        
        serviceCollection.AddSingleton<IRabbitMqStartup, RabbitMqStartup>();

        return serviceCollection;
    }
}