using System;
using System.Threading.Tasks;
using Core.Models;

namespace BLL.Abstraction.Interfaces
{
    public interface IGameManager
    {
        Task StartGameAsync(Game newGame);

        Task AddUserToGameAsync(Guid userId);

        Task FinishGameAsync(Guid winnerId);

        Task MakeStepAsync(Step step);
    }
}