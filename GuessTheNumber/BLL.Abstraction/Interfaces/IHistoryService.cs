using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Responses;

namespace BLL.Abstraction.Interfaces
{
    public interface IHistoryService
    {
        Task<HistoryManagerResponse> GetAllGames();

        Task<HistoryManagerResponse> GetGamesPlayerWon(Guid userId);

        Task<HistoryManagerResponse> GetGamesWithPlayer(Guid userId);
    }
}