using AlgorithmicTrading.Data.Database;
using AlgorithmicTrading.Data.Models;

namespace AlgorithmicTrading.Data.Repositories;

public class DateTriedRepository(DatabaseContext context) : Repository<DateTried>(context), IDateTriedRepository
{
    public IQueryable<DateTried> GetDatesInInterval(string ticker, DateTime startDate, DateTime endDate)
    {
        return _context.DatesTried.Where(dateTried => dateTried.Ticker == ticker && dateTried.Date >= startDate && dateTried.Date <= endDate);
    }
}