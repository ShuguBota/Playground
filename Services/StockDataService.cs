using Algorithmic_Trading.Models;
using Algorithmic_Trading.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Algorithmic_Trading.Services;

public class StockDataService : IStockDataService
{
    private readonly IStockDataRepository _stockDataRepository;
    private readonly IYFinanceService _yFinanceService;

    public StockDataService(IStockDataRepository stockDataRepository, IYFinanceService yFinanceService)
    {
        _stockDataRepository = stockDataRepository;
        _yFinanceService = yFinanceService;
    }

    public async Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate)
    {
        var dates = DatesService.GetDatesInRange(startDate, endDate);
        (startDate, endDate) = DatesService.EnsureDateTimeKind(startDate, endDate);

        var data = _stockDataRepository.GetStockForDates(ticker, startDate, endDate);

        if (data.Count() == dates.Count)
        {
            return await data.ToListAsync();
        }

        // TODO: Make sure that dates that were tried before are not tried agian at another request
        dates.RemoveAll(date => data.Any(stock => stock.Date == date));

        var toDownloadDates = DatesService.GetStartEndDates(dates);

        foreach (var dateRange in toDownloadDates)
        {
            try 
            {
                var downloadData = await _yFinanceService.DownloadHistoricalData(ticker, dateRange.startDate, dateRange.endDate);
                _stockDataRepository.AddRange(downloadData);
                data = data.Union(downloadData);
                break;
            } 
            catch (Exception ex) 
            {
                // TODO: Change to log and make proper exceptions
                Console.WriteLine(ex.Message);
            }
        }

        await _stockDataRepository.Save();;
        
        return await data.ToListAsync();
    }
}