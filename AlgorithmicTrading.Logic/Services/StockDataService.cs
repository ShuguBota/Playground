using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using AlgorithmicTrading.Data.Models;
using AlgorithmicTrading.Data.Repositories;

namespace AlgorithmicTrading.Logic.Services;

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

    public async Task<List<StockData>> GetStockData(List<string> tickers, DateTime startDate, DateTime endDate)
    {
        List<StockData> stocks = new();
        
        foreach(var ticker in tickers){
            stocks.AddRange(await GetStockData(ticker, startDate, endDate));
        }

        return stocks;
    }

    public async Task<List<StockData>> GetStockData(string ticker, DateTime startDate, DateTime endDate)
    {
        (startDate, endDate) = DatesService.EnsureDateTimeKind(startDate, endDate);

        List<StockData> requestedStocks = _stockDataRepository.GetStockForDates(ticker, startDate, endDate).ToList() ?? new();
        List<DateTried> datesAlreadyTried = _dateTriedRepository.GetDatesInInterval(ticker, startDate, endDate).ToList() ?? new();
        ImmutableHashSet<DateTime> datesMissing = DatesService.GetStockRelatedDatesInRange(startDate, endDate, requestedStocks, datesAlreadyTried);

        if (datesMissing.IsEmpty)
        {
            return requestedStocks;
        }

        HashSet<StockData> downloadedStocks = await DownloadAndFilter(datesMissing, ticker, startDate, endDate);

        UpdateStockDatabase(downloadedStocks);
        UpdateDatesTried(downloadedStocks, datesMissing, ticker);
        await SaveDatabaseChanges();

        requestedStocks.AddRange(downloadedStocks);

        return requestedStocks;
    }

    private async Task<HashSet<StockData>> DownloadAndFilter(ImmutableHashSet<DateTime> datesMissing, string ticker, DateTime startDate, DateTime endDate)
    {
        try
        {
            return (await _yFinanceService.DownloadHistoricalData(ticker, startDate, endDate))
                .Where(stock => datesMissing.Contains(stock.Date))
                .ToHashSet();
        }
        catch(Exception ex){
            _logger.LogWarning("An error occurred while downloading historical data: {Message}", ex.Message);
        }

        return new();
    }

    private void UpdateDatesTried(HashSet<StockData> downloadedStocks, ImmutableHashSet<DateTime> datesMissing, string ticker)
    {
        var triedDates = datesMissing.Where(date => !downloadedStocks.Any(stock => stock.Date == date))
            .Select(date => new DateTried(ticker, date));

        if(triedDates.Any()){
            _dateTriedRepository.AddRange(triedDates);
        }
    }

    private void UpdateStockDatabase(HashSet<StockData> downloadedStocks){
        if(downloadedStocks.Count != 0)
        {
            _stockDataRepository.BulkInsert([.. downloadedStocks]);
        }
    }

    private async Task SaveDatabaseChanges(){
        await _stockDataRepository.Save();
        await _dateTriedRepository.Save();
    }
}