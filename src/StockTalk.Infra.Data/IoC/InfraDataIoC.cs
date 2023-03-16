using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTalk.Application.Repositories;

namespace StockTalk.Infra.Data.IoC;

public static class InfraDataIoC
{
    public static IServiceCollection AddInfraDataIoC(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<IApplicationDbContext,ApplicationDbContext>(
            options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDb"), 
                    config 
                        => config.EnableRetryOnFailure(3)));
        
        return serviceCollection;
    }
}