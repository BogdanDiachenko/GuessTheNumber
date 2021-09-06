using System;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.Responses;

namespace BLL.Abstraction.Interfaces
{
    public interface IGameManager
    {
        Response<GameDto> StartGame(GameDto dto);

        Response<GameDto> AddUserToGame(Guid userId);

        Task<Response<GameDto>> FinishGameAsync(Guid? winnerId, Guid userId);

        Task<Response<GameDto>> MakeStepAsync(StepDto dto, Guid userId);
    }
}