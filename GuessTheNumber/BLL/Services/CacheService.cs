using System;
using BLL.Abstraction.Interfaces;
using Core.Models.DTOs;
using Core.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;
        private readonly CacheOptions cacheOptions;

        public CacheService(IMemoryCache cache, IOptions<CacheOptions> cacheOptions)
        {
            this.cache = cache;
            this.cacheOptions = cacheOptions.Value;
        }

        public void SetValue(GameDto game)
        {
            this.cache.Set("Game", game, TimeSpan.FromDays(this.cacheOptions.DaysToExpire));
        }

        public void RemoveCache()
        {
            this.cache.Remove("Game");
        }

        public bool IsGameActive(out GameDto game)
        {
            return this.TryGetValue(out game);
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