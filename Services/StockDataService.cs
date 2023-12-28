using Algorithmic_Trading.Database;
using Algorithmic_Trading.Models;
using Microsoft.EntityFrameworkCore;
using Yahoo.Finance;

namespace Algorithmic_Trading.Services;

// TODO: Refactor this class to not have the dbcontext and put all the db stuff in Database folder
public class StockDataService : IStockDataService
{
    private readonly HistoricalDataProvider _hdp;
    private readonly DatabaseContext _context;

    public StockDataService(HistoricalDataProvider hdp, DatabaseContext context)
    {
        _hdp = hdp;
        _context = context;
    }

    public async Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate)
    {
        var dates = GetDates(startDate, endDate);

        var data = await _context.Stocks
            .Where(stock => stock.Ticker == ticker && dates.Contains(stock.Date))
            .ToListAsync();

        if (data.Count == dates.Count)
        {
            return data;
        }

        dates.RemoveAll(date => data.Any(stock => stock.Date == date));
        
        // TODO: Make sure that you downlaod only the dates are not found in the db and not the whole range
        await _hdp.DownloadHistoricalDataAsync(ticker, startDate, endDate);

        if (_hdp.DownloadResult == HistoricalDataDownloadResult.Successful)
        {
            var records = _hdp.HistoricalData
                .Where(record => dates.Contains(record.Date))
                .Select(record => new StockData(ticker, record))
                .ToList();

            records.ForEach(record => record.Date = record.Date.ToUniversalTime());

            if(records.Count > 0){
                _context.Stocks.AddRange(records);
                await _context.SaveChangesAsync();
            }

            data = await _context.Stocks
                .Where(stock => stock.Ticker == ticker && stock.Date >= startDate.ToUniversalTime() && stock.Date <= endDate.ToUniversalTime())
                .ToListAsync();
            
            return data;
        }

        throw new Exception("Failed to download historical data");
    }

    // TODO: Make a way to check for holidays where the market is closed
    public List<DateTime> GetDates(DateTime startDate, DateTime endDate){
        startDate = startDate.ToUniversalTime();

        return Enumerable.Range(0, endDate.Subtract(startDate).Days + 1)
                    .Select(offset => startDate.AddDays(offset))
                    .Where(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    .ToList();
    }
}