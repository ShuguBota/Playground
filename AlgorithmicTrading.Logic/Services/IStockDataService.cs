using AlgorithmicTrading.Data.Models;

namespace AlgorithmicTrading.Logic.Services;

public interface IStockDataService
{
    Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate);
}