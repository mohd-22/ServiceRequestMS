
using Microsoft.EntityFrameworkCore;
using ServiceRequestMS.data.Data;
using ServiceRequestMS.Data.Repositories.Interfaces;
using System;
using System.Linq.Expressions;

public class GenericRepository <T> : IGenericRepository<T> where T : class
{
    protected AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
   
    public async Task<T> FindAsync(Expression<Func<T, bool>> match)
    {
        return (await _context.Set<T>().FirstOrDefaultAsync(match))!;
    }
    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match)
    {
        return await _context.Set<T>()
            .Where(match)
            .ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null)
    {
        if (criteria != null)
            return await _context.Set<T>().CountAsync(criteria);

        return await _context.Set<T>().CountAsync();
    }
    public async Task<T> GetByIdAsync(object id)
    {
        return (await _context.Set<T>().FindAsync(id))!;
    }
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }
    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }
    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }
    public async Task<IEnumerable<T>> FindAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>()
            .AsNoTracking()       
            .Where(predicate)     
            .ToListAsync();        
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }
}
