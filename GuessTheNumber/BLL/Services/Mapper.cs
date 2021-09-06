using System;
using System.Collections.Generic;
using System.Security.Claims;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.Identity;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BLL.Services
{
    public static class Mapper
    {
        public static Game ToEntity(this GameManager manager, GameDto dto)
        {
            return new()
            {
                GuessedNumber = dto.GuessedNumber,
                IsFinished = dto.IsFinished,
                HostId = dto.HostId,
                WinnerId = dto.WinnerId,
                EndTime = DateTimeOffset.Now,
                StartTime = dto.StartTime,
                Steps = ToList(dto.Steps),
                PlayersCount = dto.PlayersId.Count
            };
        }

        private static Step ToEntity(StepDto dto, int stepNumber)
        {
            return new()
            {
                Value = dto.Value,
                UserId = dto.UserId,
                Time = dto.Time,
                StepNumber = stepNumber
            };
        }

        private static List<Step> ToList(List<StepDto> dtos)
        {
            var list = new List<Step>();

            for (int i = 1; i <= dtos.Count; i++)
            {
                list.Add(ToEntity(dtos[i - 1], i));
            }

            return list;
        }
        
    }
}