﻿
namespace SchedulingCore;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    IQueryable<T> Query();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveChangesAsync();

}
