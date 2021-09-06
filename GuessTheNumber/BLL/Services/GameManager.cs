using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.Identity;
using Core.Models.Responses;
using DAL.Abstraction.Interfaces;

namespace BLL.Services
{
    public class GameManager : IGameManager
    {
        private readonly IGameRepository repository;
        private readonly ICacheService cache;


        public GameManager(IGameRepository repository, ICacheService cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        public Response<GameDto> StartGame(GameDto dto)
        {
            if (this.cache.IsGameActive(out GameDto game))
            {
                return Response<GameDto>.Failure("Current game is not finished.");
            }

            this.cache.SetValue(dto);

            return Response<GameDto>.Success(game);
        }

        public Response<GameDto> AddUserToGame(Guid userId)
        {
            if (!this.cache.IsGameActive(out GameDto game))
            {
                return Response<GameDto>.Failure("There are no active games, you should start game before");
            }

            if (game.HostId == userId)
            {
                return Response<GameDto>.Failure("You can't play the game you created");
            }

            game.PlayersId.Add(userId);
            this.cache.SetValue(game);

            return Response<GameDto>.Success(game);
        }

        public async Task<Response<GameDto>> FinishGameAsync(Guid? winnerId, Guid userId)
        {
            if (!this.cache.IsGameActive(out GameDto game))
            {
                return Response<GameDto>.Failure("There are no active games, you should start game before");
            }

            if (!this.cache.IsPlayerInGame(userId))
            {
                return Response<GameDto>.Failure("You should join the game first");
            }

            // Used Guid.Empty for calling method while number isn't guessed
            if (winnerId == Guid.Empty)
            {
                if (!this.FinishGameVote())
                {
                    return Response<GameDto>.Failure("Most players decided not to finish game");
                }

                if (game.Steps.Any())
                {
                    winnerId = this.SelectNearestToWinUser(game);
                }
                else
                {
                    winnerId = null;
                }
            }

            game.IsFinished = true;
            game.WinnerId = winnerId;
            this.cache.SetValue(game);

            await this.repository.AddAsync(this.ToEntity(game));
            this.cache.RemoveCache();

            return Response<GameDto>.Success(game);
            }

        public async Task<Response<GameDto>> MakeStepAsync(StepDto dto, Guid userId)
        {
            if (!this.cache.IsGameActive(out GameDto game))
            {
                return Response<GameDto>.Failure("There are no active games, you should start game before");
            }

            if (!this.cache.IsPlayerInGame(userId))
            {
                return Response<GameDto>.Failure("You should join the game first");
            }

            long difference = game.GuessedNumber - dto.Value;

            if (difference < 0)
            {
                return this.SaveStep(game, dto, "You should try number less than current!");
            }

            if (difference > 0)
            {
                return this.SaveStep(game, dto, "You should try number bigger than current!");
            }

            game.Steps.Add(dto);
            return await this.FinishGameAsync(userId, userId);
        }

        public Task<List<ApplicationUser>> GetPlayersListById(List<Guid> playersId)
        {
            return this.repository.GetPlayersListById(playersId);
        }

        private bool FinishGameVote()
        {
            return true;
        }

        private Guid SelectNearestToWinUser(GameDto game)
        {
            var closestStep = game.Steps.Aggregate((x, y) =>
                Math.Abs(x.Value - game.GuessedNumber) <
                Math.Abs(y.Value - game.GuessedNumber) ? x : y);

            return closestStep.UserId;
        }

        private Response<GameDto> SaveStep(GameDto game, StepDto dto, string message)
        {
            game.Steps.Add(dto);
            this.cache.SetValue(game);

            return Response<GameDto>.Success(game);
        }
    }
}