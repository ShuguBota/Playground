namespace Algorithmic_Trading.Models;

public class DateTried {
    public string Ticker { get; set; }
    public DateTime Date { get; set; }

    public DateTried(string ticker, DateTime date)
    {
        Ticker = ticker;
        Date = date;
    }
}