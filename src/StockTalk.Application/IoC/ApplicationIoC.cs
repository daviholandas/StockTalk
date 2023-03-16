using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StockTalk.Application.Behaviors;

namespace StockTalk.Application.IoC;

public static class ApplicationIoC
{
    public static IServiceCollection AddApplicationIoC(this IServiceCollection serviceCollection)
    {
        var assemblies = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(x => x.FullName!.StartsWith("StockTalk"))
            .ToArray();

        serviceCollection.AddValidatorsFromAssemblies(assemblies);
        
        serviceCollection.AddMediatR(
            config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });
        
        return serviceCollection;
    }
}