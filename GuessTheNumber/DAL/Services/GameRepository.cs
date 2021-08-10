using System;
using System.Threading.Tasks;
using Core.Models;
using DAL.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Game> entities;

        public GameRepository(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
            this.entities = context.Set<Game>();
        }

        public async Task AddUserToGameAsync(Guid gameId, Guid userId)
        {
            var game = await this.entities
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            var user = await this.context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            game.Players.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task MakeStepAsync(Step step)
        {
            var game = await this.context.Games
                .Include(g => g.Steps)
                .FirstOrDefaultAsync(g => g.Id == step.GameId);

            game.Steps.Add(step);
            await this.context.SaveChangesAsync();
        }

        public async Task FinishGameAsync(Guid gameId, Guid winnerId)
        {
            var game = await this.entities
                .Include(g => g.Players)
                .FirstOrDefaultAsync(g => g.Id == gameId);

            game.WinnerId = winnerId;
            game.EndTime = DateTimeOffset.Now;
            game.IsFinished = true;

            await this.context.SaveChangesAsync();
        }
    }
}