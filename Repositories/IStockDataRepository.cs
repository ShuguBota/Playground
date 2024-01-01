using Algorithmic_Trading.Models;

namespace Algorithmic_Trading.Repositories; 

public interface IStockDataRepository : IRepository<StockData>
{
    public IQueryable<StockData> GetStockForDates(string ticker, List<DateTime> dates);
    public IQueryable<StockData> GetStockForDates(string ticker, DateTime startDate, DateTime endDate);
}