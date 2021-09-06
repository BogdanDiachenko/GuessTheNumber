using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Identity;
using Core.Models.Responses;
using Core.Models.ViewModels;
using Core.Options;
using Core.Paging;
using DAL.Abstraction.Interfaces;
using DAL.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoreLinq;

namespace DAL.Services
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IUserRepository userRepository;

        public HistoryRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
        }

        public Task<PagedList<GameResult>> ToListAsync(PagingParams options)
        {
            var gameList = this.context.Games
                .AsQueryable();

            var source = gameList
                .Select(
                    game => game.ToGameResult(
                        this.userRepository.GetById(game.WinnerId).Result.Value.ToViewModel(),
                        this.userRepository.GetById(game.HostId).Result.ToViewModel()));
            return PagedList<GameResult>.CreateAsync(source, options.PageNumber, options.PageSize);
        }

        public Task<PagedList<GameResult>> ToListAsync(
            Expression<Func<Game, bool>> predicate,
            PagingParams options)
        {
            var gameList = this.context.Games
                .Where(predicate)
                .Skip((options.PageNumber - 1) * options.PageSize)
                .Take(options.PageSize)
                .AsQueryable();

            var source = gameList
                .Select(
                    game => game.ToGameResult(
                        this.userRepository.GetById(game.WinnerId).Result.Value.ToViewModel(),
                        this.userRepository.GetById(game.HostId).Result.ToViewModel()));
            return PagedList<GameResult>.CreateAsync(source, options.PageNumber, options.PageSize);
        }

        public Task<Game> GetGameDetailed(Guid id)
        {
            return this.context.Games.SingleOrDefaultAsync(game => game.Id == id);
        }
    }
}