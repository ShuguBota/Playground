using System.Linq.Expressions;

namespace AlgorithmicTrading.Data.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    T GetById<TId>(TId id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    Task Save();
    void Delete(T entity);
}
