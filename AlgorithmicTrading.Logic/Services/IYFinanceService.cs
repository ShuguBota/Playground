using AlgorithmicTrading.Data.Models;

namespace AlgorithmicTrading.Logic.Services;

public interface IYFinanceService
{
    public Task<IEnumerable<StockData>> DownloadHistoricalData(string ticker, DateTime startDate, DateTime endDate);
}