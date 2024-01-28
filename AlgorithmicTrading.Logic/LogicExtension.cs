
using AlgorithmicTrading.Logic.Services;
using Microsoft.Extensions.DependencyInjection;
using Yahoo.Finance;

namespace AlgorithmicTrading.Logic;

public static class LogicExtension
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services){
        services.AddScoped<IStockDataService, StockDataService>();
        services.AddScoped<ICsvService, CsvService>();

        services.AddSingleton<HistoricalDataProvider>();
        services.AddSingleton<IYFinanceService, YFinanceService>();

        return services;
    }
}