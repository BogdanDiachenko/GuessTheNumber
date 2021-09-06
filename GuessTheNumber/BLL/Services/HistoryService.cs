using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.Responses;
using Core.Models.ViewModels;
using Core.Paging;
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

        public async Task<Response<PagedList<GameResult>>> GetAllGames(PagingParams model)
        {
            var result = await this.repository.ToListAsync(model);

            return this.GenerateManagerResponse(result);
        }

        public async Task<Response<PagedList<GameResult>>> GetGamesWithPlayer(Guid userId, PagingParams model)
        {
            var result = await this.repository.ToListAsync(
                game => game
                    .Steps
                    .Select(step => step.UserId)
                    .Distinct()
                    .Contains(userId), model);

            return this.GenerateManagerResponse(result);
        }

        public async Task<Response<PagedList<GameResult>>> GetGamesPlayerWon(Guid userId, PagingParams model)
        {
            var result = await this.repository.ToListAsync(
                game => game.WinnerId == userId, model);

            return this.GenerateManagerResponse(result);
        }

        private Response<PagedList<GameResult>> GenerateManagerResponse(PagedList<GameResult> result)
        {
            if (!result.Any())
            {
                return new Response<PagedList<GameResult>>()
                {
                    IsSuccess = false,
                    Error = "Haven't founded games with this condition"
                };
            }

            return new Response<PagedList<GameResult>>()
            {
                IsSuccess = true,
                Value = result
            };
        }
    }
}