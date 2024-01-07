using Yahoo.Finance;

namespace AlgorithmicTrading.Data.Models;

public class CustomHistoricalDataRecord : HistoricalDataRecord 
{

    public CustomHistoricalDataRecord() 
    : base(){

    }
    
    public CustomHistoricalDataRecord(HistoricalDataRecord record)
    {
        Date = record.Date;
        Open = record.Open;
        High = record.High;
        Low = record.Low; 
        Close = record.Close;
        Volume = record.Volume;
        AdjustedClose = record.AdjustedClose;
    }
}