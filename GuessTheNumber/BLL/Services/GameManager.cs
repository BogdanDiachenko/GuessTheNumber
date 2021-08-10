using System;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using DAL.Abstraction.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services
{
    public class GameManager : IGameManager
    {
        private readonly MemoryCacheEntryOptions cacheOptions;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGameRepository repository;
        private IMemoryCache cache;

        public GameManager(IGameRepository repository, UserManager<ApplicationUser> userManager, IMemoryCache cache)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.cache = cache;
            this.cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };
            this.cacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
        }

        public Task StartGameAsync(Game newGame)
        {
            if (this.TryGetCache("IsFinished", out bool isFinished) && !isFinished)
            {
                return Task.CompletedTask;
            }
            newGame.Id = this.repository.AddAsync(newGame).Result.Entity.Id;
            this.SetCache(newGame);
            return this.repository.SaveChangesAsync();
        }

        public Task AddUserToGameAsync(Guid userId)
        {
            if (!(this.TryGetCache("IsFinished", out bool isFinished) && !isFinished))
            {
                return Task.CompletedTask;
            }

            this.TryGetCache("GameId", out Guid gameId);
            return this.repository.AddUserToGameAsync(gameId, userId);
        }

        public Task FinishGameAsync(Guid winnerId)
        {
            if (!(this.TryGetCache("IsFinished", out bool isFinished) && !isFinished))
            {
                return Task.CompletedTask;
            }

            this.TryGetCache("GameId", out Guid gameId);
            return this.repository.FinishGameAsync(gameId, winnerId);
        }

        public Task MakeStepAsync(Step step)
        {
            if (!(this.TryGetCache("IsFinished", out bool isFinished) && !isFinished))
            {
                return Task.CompletedTask;
            }

            this.TryGetCache("GameId", out Guid gameId);
            this.TryGetCache("GuessedNumber", out int guessedNumber);
            step.GameId = gameId;

            int difference = guessedNumber - step.Value;

            switch (difference)
            {
                case 0:
                    Console.WriteLine("You've guessed the number!!!");
                    this.cache.Set("IsFinished", true, this.cacheOptions);
                    this.repository.MakeStepAsync(step);
                    return this.repository.FinishGameAsync(gameId, step.UserId);
                case < 0:
                    Console.WriteLine("You should try number less than current!");
                    return this.repository.MakeStepAsync(step);
                case > 0:
                    Console.WriteLine("You should try number bigger than current!");
                    return this.repository.MakeStepAsync(step);
            }
        }

        private void SetCache(Game game)
        {
            this.cache.Set("GuessedNumber", game.GuessedNumber, this.cacheOptions);
            this.cache.Set("IsFinished", game.IsFinished, this.cacheOptions);
            this.cache.Set("GameId", game.Id, this.cacheOptions);
        }

        private bool TryGetCache<T>(string prop, out T result)
        {
            return this.cache.TryGetValue(prop, out result);
        }
    }
}