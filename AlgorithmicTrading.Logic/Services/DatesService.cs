using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Algorithmic_Trading.Models;

namespace Algorithmic_Trading.Services;

public class DatesService
{
    // TODO: Make a way to check for holidays where the market is closed
    public static IEnumerable<DateTime> GetDatesInRange(DateTime startDate, DateTime endDate)
    {
        if(startDate.Kind != DateTimeKind.Utc){
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        }

        return Enumerable.Range(0, (endDate - startDate).Days + 1)
            .Select(offset => startDate.AddDays(offset))
            .ToList();
    }

    public static ImmutableHashSet<DateTime> GetStockRelatedDatesInRange(
        DateTime startDate, DateTime endDate, List<StockData> data, List<DateTried> datesAlreadyTried)
    {
        return GetDatesInRange(startDate, endDate)
            .Where(date => !data.Any(stock => stock.Date == date))
            .Where(IsWeekday)
            .Where(date => !datesAlreadyTried.Any(dateTried => dateTried.Date == date))
            .ToImmutableHashSet();
    }

    public static StockData EnsureDateTimeKind(StockData stockData){
        if(stockData.Date.Kind != DateTimeKind.Utc){
            stockData.Date = DateTime.SpecifyKind(stockData.Date, DateTimeKind.Utc);
        }

        return stockData;
    }

    public static (DateTime, DateTime) EnsureDateTimeKind(DateTime startDate, DateTime endDate){
        if(startDate.Kind != DateTimeKind.Utc){
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        }
        
        if(endDate.Kind != DateTimeKind.Utc){
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);
        }

        return (startDate, endDate);
    }

    public static List<DateTime> EnsureDateTimeKind(List<DateTime> dates){
        dates.ForEach(date => {
            if(date.Kind != DateTimeKind.Utc){
                date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            }
        });

        return dates;
    }

    public static List<(DateTime startDate, DateTime endDate)> GetStartEndDates(List<DateTime> dates){
        if(dates.Count == 0){
            return new List<(DateTime startDate, DateTime endDate)>();
        }

        return dates
            .Select((date, index) => new { date, index })
            .GroupBy(x => x.date.Date.AddDays(-x.index))
            .Select(group => (group.First().date, group.Last().date))
            .ToList();
    }

    public static bool IsWeekend(DateTime date){
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    public static bool IsWeekday(DateTime date){
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }
}