﻿using AppControle.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        _context.Set<T>()
            .Update(entity);
        //_context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
    public T Delete(T entity)
    {
        _context.Set<T>()
            .Remove(entity);
        return entity;
    }
}
