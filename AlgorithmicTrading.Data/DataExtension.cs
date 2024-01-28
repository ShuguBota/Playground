using AlgorithmicTrading.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Algorithmic_Trading.Data;

public static class DataExtension
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IStockDataRepository, StockDataRepository>();
        services.AddScoped<IDateTriedRepository, DateTriedRepository>();
        
        return services;
    }
}