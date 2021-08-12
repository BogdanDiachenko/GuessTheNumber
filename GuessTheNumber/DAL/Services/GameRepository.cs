using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Identity;
using DAL.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly ApplicationDbContext context;

        public GameRepository(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
        }

        // public async Task AddUserToGameAsync(Guid userId)
        // {
        //     var game = await this.entities
        //         .Include(g => g.Players)
        //         .FirstOrDefaultAsync(g => !g.IsFinished);
        //
        //     var user = await this.context.Users
        //         .FirstOrDefaultAsync(u => u.Id == userId);
        //
        //     game.Players.Add(user);
        //     await this.context.SaveChangesAsync();
        // }
        //
        // public async Task MakeStepAsync(Step step)
        // {
        //     var game = await this.context.Games
        //         .Include(g => g.Steps)
        //         .FirstOrDefaultAsync(g => g.Id == step.GameId);
        //
        //     game.Steps.Add(step);
        //     await this.context.SaveChangesAsync();
        // }
        //
        // public async Task FinishGameAsync(Guid winnerId)
        // {
        //     var game = await this.entities
        //         .Include(g => g.Players)
        //         .FirstOrDefaultAsync(g => !g.IsFinished);
        //
        //     game.WinnerId = winnerId;
        //     game.EndTime = DateTimeOffset.Now;
        //     game.IsFinished = true;
        //     await this.context.SaveChangesAsync();
        // }
        //
        // public Task<Game> GetActiveGame()
        // {
        //     return this.context.Games.FirstOrDefaultAsync(g => g.IsFinished);
        // }

        public async Task<List<ApplicationUser>> GetPlayersListById(List<Guid> playersId)
        {
            var list = new List<ApplicationUser>();
            foreach (var id in playersId)
            {
                list.Add(await this.context.Users.FindAsync(id));
            }

            return list;
        }
    }
}