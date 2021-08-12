// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using System.Threading.Tasks;
// using BLL.Abstraction.Interfaces;
// using Core.Models;
// using DAL.Abstraction.Interfaces;
//
// namespace BLL.Services
// {
//     public class GenericService<T> : IGenericService<T>
//         where T : class, IBaseEntity<Guid>
//     {
//         private readonly IGenericRepository<T> repository;
//
//         public GenericService(IGenericRepository<T> repository)
//         {
//             this.repository = repository;
//         }
//
//         public Task<T> GetByIdAsync(Guid id)
//         {
//             return this.repository.GetByIdAsync(id);
//         }
//
//         public Task AddAsync(T entity)
//         {
//             return this.repository.AddAsync(entity);
//         }
//
//         public Task UpdateAsync(T entity)
//         {
//             return this.repository.UpdateAsync(entity);
//         }
//
//         public Task DeleteAsync(T entity)
//         {
//             return this.repository.DeleteAsync(entity);
//         }
//
//         public Task<List<T>> ToListAsync()
//         {
//             return this.repository.ToListAsync();
//         }
//
//         public Task<List<T>> ToListASync(Expression<Func<T, bool>> predicate)
//         {
//             return this.repository.ToListAsync();
//         }
//     }
// }