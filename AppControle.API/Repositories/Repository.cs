using AppControle.API.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using System.Linq.Expressions;
using System.Security.Claims;

namespace APICatalogo.Repositories;
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DataContext _context;

    public Repository(DataContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAllNoPaginationAsync()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>()
            .Add(entity);
        return entity;
    }
    public T Update(T entity)
    {
        //_context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>()
            .Update(entity);
        return entity;
    }
    public T Delete(T entity)
    {
        _context.Set<T>()
            .Remove(entity);
        return entity;
    }


}
