using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.BaseEntity;
using DAL.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Services
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IBaseEntity<Guid>
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = this.context.Set<T>();
        }

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return this.entities.FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task AddAsync(T entity)
        {
            this.entities.AddAsync(entity);
            return this.context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            return this.context.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            this.entities.Remove(entity);
            return this.context.SaveChangesAsync();
        }

        public Task<List<T>> ToListAsync()
        {
            return this.entities.ToListAsync();
        }

        public virtual Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate)
        {
            return this.entities.Where(predicate).ToListAsync();
        }
    }
}