using AlgorithmicTrading.Data.Database;
using AlgorithmicTrading.Data.Models;
using EFCore.BulkExtensions;

namespace AlgorithmicTrading.Data.Repositories;

public class StockDataRepository(DatabaseContext _context) : Repository<StockData>(_context), IStockDataRepository
{
    public IQueryable<StockData> GetStockForDates(string ticker, List<DateTime> dates)
    {
        return _context.Stocks.Where(stock => stock.Ticker == ticker && dates.Contains(stock.Date));
    }

    public IQueryable<StockData> GetStockForDates(string ticker, DateTime startDate, DateTime endDate)
    {
        return _context.Stocks.Where(stock => stock.Ticker == ticker && stock.Date >= startDate && stock.Date <= endDate);
    }

    public void BulkInsert(List<StockData> data)
    {
        _context.BulkInsert(data);
    }
}