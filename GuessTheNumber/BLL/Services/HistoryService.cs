using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using DAL.Abstraction.Interfaces;

namespace BLL.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository repository;

        public HistoryService(IHistoryRepository repository)
        {
            this.repository = repository;
        }

        public Task<List<Game>> GetFinishedGamesAsync()
        {
            return this.repository.ToListAsync(g => g.IsFinished);
        }

        public Task<List<Game>> GetGamesWithPlayer(Guid userId)
        {
            return this.repository.ToListAsync(g =>
                g.Players.First(u => u.Id == userId) != null);
        }

        public async Task<List<Game>> GetGamesPlayerWon(Guid userId)
        {
            var list = await this.GetGamesWithPlayer(userId);
            return list.FindAll(g => g.WinnerId == userId);
        }
    }
}