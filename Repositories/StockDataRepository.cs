using Algorithmic_Trading.Database;
using Algorithmic_Trading.Models;

namespace Algorithmic_Trading.Repositories;

public class StockDataRepository : Repository<StockData>, IStockDataRepository
{
    public StockDataRepository(DatabaseContext _context) 
    : base(_context)
    {
    }

    public IQueryable<StockData> GetStockForDates(string ticker, List<DateTime> dates)
    {
        return _context.Stocks.Where(stock => stock.Ticker == ticker && dates.Contains(stock.Date));
    }

    public IQueryable<StockData> GetStockForDates(string ticker, DateTime startDate, DateTime endDate)
    {
        return _context.Stocks.Where(stock => stock.Ticker == ticker && stock.Date >= startDate && stock.Date <= endDate);
    }
}