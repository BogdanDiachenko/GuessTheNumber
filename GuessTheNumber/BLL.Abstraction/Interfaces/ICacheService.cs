using System;
using Core.Models.DTOs;

namespace BLL.Abstraction.Interfaces
{
    public interface ICacheService
    {
        void SetValue(GameDto game);

        bool IsGameActive(out GameDto game);

        bool IsPlayerInGame(Guid userId);
    }
}