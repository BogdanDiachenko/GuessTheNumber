using System;
using System.Threading.Tasks;
using Core.Models;

namespace DAL.Abstraction.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task AddUserToGameAsync(Guid gameId, Guid userId);

        Task FinishGameAsync(Guid gameId, Guid winnerId);

        Task MakeStepAsync(Step step);
    }
}