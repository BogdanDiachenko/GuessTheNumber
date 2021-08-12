using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace BLL.Abstraction.Interfaces
{
    public interface IHistoryService
    {
        Task<List<Game>> GetFinishedGamesAsync();

        Task<List<Game>> GetGamesPlayerWon(Guid userId);

        Task<List<Game>> GetGamesWithPlayer(Guid userId);
    }
}