using System;
using BLL.Abstraction.Interfaces;
using Core.Models.DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace BLL.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;
        private readonly MemoryCacheEntryOptions options;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache;
            this.options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };
            this.options.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
        }

        public void SetValue(GameDto game)
        {
            this.cache.Set("Game", game, this.options);
        }

        public bool IsGameActive(out GameDto game)
        {
            return this.TryGetValue(out game) && game.IsFinished == false;
        }

        public bool IsPlayerInGame(Guid userId)
        {
            this.TryGetValue(out GameDto game);
            return game.PlayersId.Contains(userId);
        }
        
        private bool TryGetValue(out GameDto game)
        {
            return this.cache.TryGetValue("Game", out game);
        }
    }
}