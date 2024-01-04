using Algorithmic_Trading.Models;

namespace Algorithmic_Trading.Repositories;

public interface IDateTriedRepository : IRepository<DateTried>
{
    public IQueryable<DateTried> GetDatesInInterval(string ticker, DateTime startDate, DateTime endDate);
}