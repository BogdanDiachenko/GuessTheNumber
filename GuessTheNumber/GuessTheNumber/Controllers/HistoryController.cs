using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllGames()
        {
            var history = await this.service.GetAllGames();

            if (history == null)
            {
                return this.BadRequest("There are no finished games.");
            }

            return this.Ok(history);
        }

        [HttpGet("GetGamesWithPlayer")]
        public async Task<IActionResult> GetGamesWithPlayer()
        {
            var userId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var history = await this.service.GetGamesWithPlayer(userId);

            if (history == null)
            {
                return this.BadRequest("There are no finished games with you.");
            }

            return this.Ok(history);
        }

        [HttpGet("GetGamesPlayerWon")]
        public async Task<IActionResult> GetGamesPlayerWon()
        {
            var userId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var history = await this.service.GetGamesPlayerWon(userId);

            if (history == null)
            {
                return this.BadRequest("There are no finished games you won.");
            }

            return this.Ok(history);
        }
    }
}