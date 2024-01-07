using AlgorithmicTrading.Data.Models;

namespace AlgorithmicTrading.Data.Repositories;

public interface IStockDataRepository : IRepository<StockData>
{
    public IQueryable<StockData> GetStockForDates(string ticker, List<DateTime> dates);
    public IQueryable<StockData> GetStockForDates(string ticker, DateTime startDate, DateTime endDate);
    public void BulkInsert(List<StockData> data);
}