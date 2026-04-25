using ServiceRequestMS.Core.Models;
using System.Linq.Expressions;

namespace ServiceRequestMS.Data.Repositories.Interfaces;
public interface IGenericRepository <T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> FindAsync(Expression<Func<T, bool>> match);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match);
    Task<T> GetByIdAsync(object id);
    Task<T> AddAsync(T entity);
    T Delete(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null);
    T Update(T entity);
    Task<IEnumerable<T>> FindAsNoTrackingAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
}
