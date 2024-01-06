using Algorithmic_Trading.Models;

namespace Algorithmic_Trading.Services;

public interface IYFinanceService
{
    public Task<IEnumerable<StockData>> DownloadHistoricalData(string ticker, DateTime startDate, DateTime endDate);
}