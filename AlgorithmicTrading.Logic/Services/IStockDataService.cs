using Algorithmic_Trading.Models;

public interface IStockDataService
{
    Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate);
}