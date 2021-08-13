using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.Responses;
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

        public async Task<HistoryManagerResponse> GetAllGames()
        {
            var result = await this.repository.ToListAsync();

            return this.GenerateManagerResponse(result);
        }

        public async Task<HistoryManagerResponse> GetGamesWithPlayer(Guid userId)
        {
            var result = await this.repository.ToListAsync(ug => ug.UserId == userId);

            return this.GenerateManagerResponse(result);
        }

        public async Task<HistoryManagerResponse> GetGamesPlayerWon(Guid userId)
        {
            var list = await this.GetGamesWithPlayer(userId);
            var result = list.History.FindAll(ug => ug.Game.WinnerId == userId);

            return this.GenerateManagerResponse(result);
        }

        private HistoryManagerResponse GenerateManagerResponse(List<UserGame> result)
        {
            if (result.Count < 1)
            {
                return new HistoryManagerResponse()
                {
                    IsSuccess = false,
                    Message = "Haven't founded games with this condition"
                };
            }

            return new HistoryManagerResponse()
            {
                IsSuccess = true,
                Message = "Games  were successfully founded",
                History = result,
            };
        }
    }
}