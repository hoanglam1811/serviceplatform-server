using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Repository.Implement;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ServiceSolutionDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository()
    {
        _context = new ServiceSolutionDbContext();
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
        {
            query = include(query);
        }

        return await query.AsNoTracking().ToListAsync();
    }


    public async Task<T?> GetByIdAsync(params object[] keys)
    {
        var entity = await _dbSet.FindAsync(keys);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }


    public async Task<T> AddAsync(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }
        finally
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        finally
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        finally
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        return entity;
    }

	public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
	{
		return await _dbSet.FirstOrDefaultAsync(predicate);
	}
}
