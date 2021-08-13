using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Identity;
using Core.Models.Responses;

namespace DAL.Abstraction.Interfaces
{
    public interface IGameRepository
    {
        Task<List<ApplicationUser>> GetPlayersListById(List<Guid> playersId);

        Task AddAsync(Game game);
    }
}