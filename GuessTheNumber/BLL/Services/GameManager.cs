using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.DTOs;
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

        public GameManagerResponse StartGame(GameDto dto)
        {
            if (this.cache.IsGameActive(out GameDto game))
            {
                return new GameManagerResponse()
                {
                    Message = "Current game is not finished.",
                    IsSuccess = false
                };
            }

            this.cache.SetValue(dto);

            return new GameManagerResponse()
            {
                IsSuccess = true,
                Message = "Game has successfully added."
            };
        }

        public GameManagerResponse AddUserToGame(Guid userId)
        {
            if (!this.cache.IsGameActive(out GameDto game))
            {
                return new GameManagerResponse()
                {
                    IsSuccess = false,
                    Message = "There are no active games, you should start game before."
                };
            }

            if (game.HostId == userId)
            {
                return new GameManagerResponse()
                {
                    IsSuccess = false,
                    Message = "You can't play the game you created."
                };
            }

            game.PlayersId.Add(userId);
            this.cache.SetValue(game);

            return new GameManagerResponse()
            {
                Message = "Enjoy the game!",
                IsSuccess = true
            };
        }

        public async Task<GameManagerResponse> FinishGameAsync(Guid winnerId, Guid userId)
        {
            if (!this.cache.IsGameActive(out GameDto game))
            {
                return new GameManagerResponse()
                {
                    IsSuccess = false,
                    Message = "There are no active games, you should start game before."
                };
            }

            if (!this.cache.IsPlayerInGame(userId))
            {
                return new GameManagerResponse()
                {
                    IsSuccess = false,
                    Message = "You should join the game first."
                };
            }

            // Used Guid.Empty for calling method while number isn't guessed
            if (winnerId == Guid.Empty)
            {
                if (!this.FinishGameVote())
                {
                    return new GameManagerResponse()
                    {
                        IsSuccess = false,
                        Message = "Most players decided not to finish game."
                    };
                }

                winnerId = this.SelectNearestToWinUser(game);
            }

            game.IsFinished = true;
            game.WinnerId = winnerId;
            this.cache.SetValue(game);

            await this.repository.AddAsync(this.ToEntity(game));

            if (winnerId != userId)
            {
                return new GameManagerResponse()
                {
                    IsSuccess = true,
                    Message = "Game has ended."
                };
            }

            return new GameManagerResponse()
            {
                IsSuccess = true,
                Message = "Congratulations, you won!!!"
            };
        }

        public async Task<GameManagerResponse> MakeStepAsync(StepDto dto, Guid userId)
        {
            if (!this.cache.IsGameActive(out GameDto game))
            {
                return new GameManagerResponse()
                {
                    IsSuccess = false,
                    Message = "There are no active games, you should start game before."
                };
            }

            if (!this.cache.IsPlayerInGame(userId))
            {
                return new GameManagerResponse()
                {
                    IsSuccess = false,
                    Message = "You should join the game first."
                };
            }

            int difference = game.GuessedNumber - dto.Value;

            if (difference < 0)
            {
                game.Steps.Add(dto);
                this.cache.SetValue(game);

                return new GameManagerResponse()
                {
                    IsSuccess = true,
                    Message = "You should try number less than current!"
                };
            }

            if (difference > 0)
            {
                game.Steps.Add(dto);
                this.cache.SetValue(game);

                return new GameManagerResponse()
                {
                    Message = "You should try number bigger than current!",
                    IsSuccess = true
                };
            }

            game.Steps.Add(dto);
            return await this.FinishGameAsync(userId, userId);
        }

        private Game ToEntity(GameDto dto)
        {
            return new()
            {
                GuessedNumber = dto.GuessedNumber,
                IsFinished = dto.IsFinished,
                HostId = dto.HostId,
                WinnerId = dto.WinnerId,
                EndTime = DateTimeOffset.Now,
                StartTime = dto.StartTime,
                Steps = this.ToList(dto.Steps),
                Players = this.repository.GetPlayersListById(dto.PlayersId).Result,
            };
        }

        private Step ToEntity(StepDto dto)
        {
            return new()
            {
                Value = dto.Value,
                UserId = dto.UserId,
                Time = dto.Time,
            };
        }

        private List<Step> ToList(List<StepDto> dtos)
        {
            var result = new List<Step>();
            foreach (var dto in dtos)
            {
                result.Add(this.ToEntity(dto));
            }

            return result;
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
    }
}