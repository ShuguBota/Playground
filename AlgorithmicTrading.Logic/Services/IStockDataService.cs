using AlgorithmicTrading.Data.Models;

namespace AlgorithmicTrading.Logic.Services;

public interface IStockDataService
{
    Task<List<StockData>> GetStockData(List<string> tickers, DateTime startDate, DateTime endDate);

    Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate);
}