using System.Linq.Expressions;
using Algorithmic_Trading.Database;
using Microsoft.EntityFrameworkCore;

namespace Algorithmic_Trading.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DatabaseContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DatabaseContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate).ToList();
    }

    public T GetById<TId>(TId id)
    {
        return _dbSet.Find(id) ?? throw new Exception("Entity not found");
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}