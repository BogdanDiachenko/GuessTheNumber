using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BLL.Abstraction.Interfaces
{
    public interface IGenericService<T> where T : IBaseEntity<Guid>
    {
        Task<T> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<List<T>> ToListAsync();

        Task<List<T>> ToListASync(Expression<Func<T, bool>> predicate);
    }
}