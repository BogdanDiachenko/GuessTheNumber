using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;

namespace DAL.Abstraction.Interfaces
{
    public interface IHistoryRepository
    {
        Task<List<Game>> ToListAsync(Expression<Func<Game, bool>> predicate);
    }
}