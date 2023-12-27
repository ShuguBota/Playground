using Yahoo.Finance;

namespace Algorithmic_Trading.Models;

public class StockData : HistoricalDataRecord 
{
    public required string Ticker { get; set; }
}