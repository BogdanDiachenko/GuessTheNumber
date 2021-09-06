using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Responses;
using Core.Models.ViewModels;
using Core.Paging;

namespace BLL.Abstraction.Interfaces
{
    public interface IHistoryService
    {
        Task<Response<PagedList<GameResult>>> GetAllGames(PagingParams model);

        Task<Response<PagedList<GameResult>>> GetGamesPlayerWon(Guid userId, PagingParams model);

        Task<Response<PagedList<GameResult>>> GetGamesWithPlayer(Guid userId, PagingParams model);
    }
}