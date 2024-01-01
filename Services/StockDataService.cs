using Algorithmic_Trading.Models;
using Algorithmic_Trading.Repositories;
using Microsoft.EntityFrameworkCore;
using Yahoo.Finance;

namespace Algorithmic_Trading.Services;

public class StockDataService : IStockDataService
{
    private readonly HistoricalDataProvider _hdp;
    private readonly IStockDataRepository _stockDataRepository;

    public StockDataService(HistoricalDataProvider hdp, IStockDataRepository stockDataRepository)
    {
        _hdp = hdp;
        _stockDataRepository = stockDataRepository;
    }

    public async Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate)
    {
        var dates = DatesService.GetDatesInRange(startDate, endDate);
        (startDate, endDate) = DatesService.EnsureDateTimeKind(startDate, endDate);

        var data = await _stockDataRepository.GetStockForDates(ticker, dates).ToListAsync();

        if (data.Count == dates.Count)
        {
            return data;
        }

        dates.RemoveAll(date => data.Any(stock => stock.Date == date));
        
        // TODO: Make sure that you downlaod only the dates are not found in the db and not the whole range
        await _hdp.DownloadHistoricalDataAsync(ticker, startDate, endDate);

        // TODO: Make this a separate service
        if (_hdp.DownloadResult == HistoricalDataDownloadResult.Successful)
        {
            var records = _hdp.HistoricalData
                .Where(record => dates.Contains(record.Date))
                .Select(record => new StockData(ticker, record))
                .ToList();

            records.ForEach(record => record.Date = record.Date.ToUniversalTime());

            if(records.Count > 0){
                _stockDataRepository.AddRange(records);
                await _stockDataRepository.Save();
            }

            data = await _stockDataRepository.GetStockForDates(ticker, startDate, endDate).ToListAsync();
            
            return data;
        }

        throw new Exception("Failed to download historical data");
    }
}