using AlgorithmicTrading.Data.Models;

namespace AlgorithmicTrading.Data.Repositories;

public interface IDateTriedRepository : IRepository<DateTried>
{
    public IQueryable<DateTried> GetDatesInInterval(string ticker, DateTime startDate, DateTime endDate);
}