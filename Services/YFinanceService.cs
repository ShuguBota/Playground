using Algorithmic_Trading.Models;
using Yahoo.Finance;

namespace Algorithmic_Trading.Services;

public class YFinanceService : IYFinanceService
{    
    private readonly HistoricalDataProvider _hdp;

    public YFinanceService(HistoricalDataProvider hdp)
    {
        _hdp = hdp;   
    }

    public async Task<IEnumerable<StockData>> DownloadHistoricalData(string ticker, DateTime startDate, DateTime endDate)
    {
        await _hdp.DownloadHistoricalDataAsync(ticker, startDate, endDate);

        if (_hdp.DownloadResult == HistoricalDataDownloadResult.Successful)
        {
            return _hdp.HistoricalData
                .Select(record => new StockData(ticker, record))
                .Select(DatesService.EnsureDateTimeKind);
        }

        if (_hdp.DownloadResult == HistoricalDataDownloadResult.NoDataFound)
        {
            Console.WriteLine($"Server side error or there's no data for that period, for ticker {ticker} from {startDate} to {endDate}");

            return Enumerable.Empty<StockData>();
        }
        
        throw new Exception($"Failed to download historical data with code {_hdp.DownloadResult}, for ticker {ticker} from {startDate} to {endDate}");
    }
}