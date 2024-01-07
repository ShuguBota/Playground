using Yahoo.Finance;

namespace AlgorithmicTrading.Data.Models;

public class StockData : CustomHistoricalDataRecord 
{
    public string Ticker { get; set; }

    public StockData()
    : base(new CustomHistoricalDataRecord())
    {
        Ticker = "";
    }

    public StockData(string ticker, HistoricalDataRecord record)
    : base(record)
    {
        Ticker = ticker;
    }
}