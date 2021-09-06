using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.ViewModels;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuessTheNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService service;

        public HistoryController(IHistoryService service)
        {
            this.service = service;
        }

        [HttpGet("GetAllGames")]
        public async Task<IActionResult> GetAllGames([FromQuery] PagingParams parameters)
        {
            var response = await this.service.GetAllGames(parameters);

            if (!response.IsSuccess)
            {
                return this.BadRequest(response);
            }

            this.Response.AddPaginationHeader(response.Value.CurrentPage, response.Value.PageSize,
                response.Value.TotalCount, response.Value.TotalPages);
            return this.Ok(response.Value);
        }

        [HttpGet("GetGamesWithPlayer")]
        public async Task<IActionResult> GetGamesWithPlayer([FromQuery] PagingParams parameters)
        {
            var userId = this.HttpContext.GetUserId();
            var response = await this.service.GetGamesWithPlayer(userId, parameters);

            if (response.Value == null)
            {
                return this.BadRequest("There are no finished games with you.");
            }

            this.Response.AddPaginationHeader(response.Value.CurrentPage, response.Value.PageSize,
                response.Value.TotalCount, response.Value.TotalPages);
            return this.Ok(response);
        }

        [HttpGet("GetGamesPlayerWon")]
        public async Task<IActionResult> GetGamesPlayerWon([FromQuery] PagingParams parameters)
        {
            var userId = this.HttpContext.GetUserId();
            var response = await this.service.GetGamesPlayerWon(userId, parameters);
            if (response.Value == null)
            {
                return this.BadRequest("There are no finished games you won.");
            }

            this.Response.AddPaginationHeader(response.Value.CurrentPage, response.Value.PageSize,
                response.Value.TotalCount, response.Value.TotalPages);
            return this.Ok(response);
        }
    }
}