using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;

namespace DAL.Abstraction.Interfaces
{
    public interface IHistoryRepository
    { 
        Task<List<UserGame>> ToListAsync(Expression<Func<UserGame, bool>> predicate);

        Task<List<UserGame>> ToListAsync();
    }
}