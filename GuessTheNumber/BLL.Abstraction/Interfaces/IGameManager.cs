using System;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.Responses;

namespace BLL.Abstraction.Interfaces
{
    public interface IGameManager
    {
        GameManagerResponse StartGame(GameDto dto);

        GameManagerResponse AddUserToGame(Guid userId);

        Task<GameManagerResponse> FinishGameAsync(Guid winnerId, Guid userId);

        Task<GameManagerResponse> MakeStepAsync(StepDto dto, Guid userId);
    }
}