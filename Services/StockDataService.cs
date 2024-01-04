using Algorithmic_Trading.Models;
using Algorithmic_Trading.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Algorithmic_Trading.Services;

public class StockDataService : IStockDataService
{
    private readonly ILogger<StockDataService> _logger;
    private readonly IStockDataRepository _stockDataRepository;
    private readonly IDateTriedRepository _dateTriedRepository;
    private readonly IYFinanceService _yFinanceService;

    public StockDataService(ILogger<StockDataService> logger, IStockDataRepository stockDataRepository, IDateTriedRepository dateTriedRepository, IYFinanceService yFinanceService)
    {
        _logger = logger;
        _stockDataRepository = stockDataRepository;
        _dateTriedRepository = dateTriedRepository;
        _yFinanceService = yFinanceService;
    }

    public async Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate)
    {
        (startDate, endDate) = DatesService.EnsureDateTimeKind(startDate, endDate);

        var data = _stockDataRepository.GetStockForDates(ticker, startDate, endDate);
        var datesAlreadyTried = _dateTriedRepository.GetDatesInInterval(ticker, startDate, endDate);
        var dates = DatesService.GetDatesInRange(startDate, endDate)
            .Where(date => !data.Any(stock => stock.Date == date))
            .Where(DatesService.IsWeekday)
            .Where(date => !datesAlreadyTried.Any(dateTried => dateTried.Date == date));

        var x = data.ToList();
        var y = dates.ToList();
        var z = datesAlreadyTried.ToList();

        if (!dates.Any())
        {
            return await data.ToListAsync();
        }

        var result = await data.ToListAsync();

        try 
        {
            var downloadData = (await _yFinanceService.DownloadHistoricalData(ticker, startDate, endDate))
                // TODO: Make this filtering more efficient
                .Where(stock => dates.Contains(stock.Date))
                .ToList();

            if(downloadData.Count != 0)
            {
                _stockDataRepository.BulkInsert(downloadData);
                result.AddRange(downloadData);
            }

            var triedDates = dates.Where(date => !downloadData.Any(stock => stock.Date == date))
                .Select(date => new DateTried(ticker, date));

            if(triedDates.Any()){
                _dateTriedRepository.AddRange(triedDates);
            }
        } 
        catch (Exception ex) 
        {
            _logger.LogWarning("An error occurred while downloading historical data: {Message}", ex.Message);
        }

        await _stockDataRepository.Save();
        await _dateTriedRepository.Save();

        return result;
    }
}