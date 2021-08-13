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

        public async Task<List<ApplicationUser>> GetPlayersListById(List<Guid> playersId)
        {
            var list = new List<ApplicationUser>();
            foreach (var id in playersId)
            {
                list.Add(await this.context.Users.FindAsync(id));
            }

            return list;
        }

        public Task AddAsync(Game game)
        {
            this.context.Games.AddAsync(game);
            return this.context.SaveChangesAsync();
        }
    }
}