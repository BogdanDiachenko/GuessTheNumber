using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Abstraction.Interfaces
{
    public interface IGenericRepository<T>
        where T : class, IBaseEntity<Guid>
    {
        Task<T> GetByIdAsync(Guid id);

        ValueTask<EntityEntry<T>> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task SaveChangesAsync();

        Task<List<T>> ToListAsync();

        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate);
    }
}