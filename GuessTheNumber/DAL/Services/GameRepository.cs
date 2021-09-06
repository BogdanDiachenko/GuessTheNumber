using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Identity;
using DAL.Abstraction.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext context;

        public GameRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<List<ApplicationUser>> GetPlayersListById(List<Guid> playersId)
        {
            return this.context.Users.Where(x => playersId.Contains(x.Id)).ToListAsync();
        }

        public Task AddAsync(Game game)
        {
            this.context.Games.AddAsync(game);
            return this.context.SaveChangesAsync();
        }
    }
}