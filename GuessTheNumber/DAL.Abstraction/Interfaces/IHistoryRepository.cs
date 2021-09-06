using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Paging;

namespace DAL.Abstraction.Interfaces
{
    public interface IHistoryRepository
    {
        Task<PagedList<GameResult>> ToListAsync(Expression<Func<Game, bool>> predicate, PagingParams options);

        Task<PagedList<GameResult>> ToListAsync(PagingParams options);

        Task<Game> GetGameDetailed(Guid id);
    }
}