using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Identity;

namespace DAL.Abstraction.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        // Task AddUserToGameAsync(Guid userId);
        //
        // Task FinishGameAsync(Guid winnerId);
        //
        // Task MakeStepAsync(Step step);
        //
        // Task<Game> GetActiveGame();

        Task<List<ApplicationUser>> GetPlayersListById(List<Guid> playersId);
    }
}