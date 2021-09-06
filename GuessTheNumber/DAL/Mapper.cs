#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Identity;
using Core.Models.Responses;
using Core.Models.ViewModels;
using DAL.Abstraction.Interfaces;
using DAL.Services;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class Mapper
    {
        public static UserViewModel ToViewModel(this ApplicationUser user)
        {
            return new()
            {
                UserId = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName
            };
        }

        public static GameResult ToGameResult(this Game game, UserViewModel winner, UserViewModel host)
        {
            return new GameResult()
            {
                GameId = game.Id,
                GuessedNumber = game.GuessedNumber,
                Winner = winner,
                Host = host,
                StartTime = game.StartTime,
                EndTime = game.EndTime,
                PlayersCount = game.PlayersCount
            };
        }
    }
}