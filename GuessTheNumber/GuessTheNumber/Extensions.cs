using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Services;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.Identity;
using Core.Models.ViewModels;
using DAL;
using DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GuessTheNumber
{
    public static class Extensions
    {
        public static GameDto ToDto(this ControllerBase context, GameViewModel model)
        {
            return new()
            {
                HostId = Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value),
                GuessedNumber = model.GuessedNumber,
                IsFinished = false,
                PlayersId = new List<Guid>(),
                Steps = new List<StepDto>(),
                StartTime = DateTimeOffset.Now
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
            };
        }
        
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages,
            };
            
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Header", "Pagination");
        }

        public static Guid GetUserId(this HttpContext context)
        {
            var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (context.User.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(claim.Value);
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

            for (int i = 1; i >= dtos.Count; i++)
            {
                list.Add(ToEntity(dtos[i - 1], i));
            }

            return list;
        }

        public static Task<ApplicationUser> GetPlayerById(this ApplicationDbContext context, Guid id)
        {
            return context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

    }
}