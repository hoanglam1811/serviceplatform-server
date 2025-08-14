using System.Linq.Expressions;

namespace Repository.Repository.Interface;
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<T?> GetByIdAsync(params object[] keys);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
	Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
}
