using System;
using System.Collections.Generic;
using System.Security.Claims;
using Core.Models.DTOs;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace GuessTheNumber
{
    public static class Extensions
    {
        public static GameDto ToDto(this ControllerBase context, GameViewModel model)
        {
            return new GameDto()
            {
                HostId = Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                GuessedNumber = model.GuessedNumber,
                IsFinished = false,
                PlayersId = new List<Guid>(),
                Steps = new List<StepDto>()
            };
        }

        public static StepDto ToDto(this ControllerBase context, StepViewModel model)
        {
            return new()
            {
                Value = model.Value,
                UserId = Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Time = DateTimeOffset.Now
            };
        }

        
    }
}