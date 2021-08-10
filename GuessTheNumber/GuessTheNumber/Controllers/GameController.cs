using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class GameController : ControllerBase
    {
        private readonly IGameManager gameManager;
        private readonly UserManager<ApplicationUser> userManager;

        public GameController(IGameManager gameManager, UserManager<ApplicationUser> userManager)
        {
            this.gameManager = gameManager;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("StartGame")]
        public async Task<IActionResult> StartGame([FromBody] GameDto game)
        {
            var newGame = new Game()
            {
                IsFinished = false,
                StartTime = DateTimeOffset.Now,
                HostId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                GuessedNumber = game.GuessedNumber,
            };

            await this.gameManager.StartGameAsync(newGame);
            return this.Ok();
        }

        [HttpPost]
        [Route("JoinGame")]
        public async Task<IActionResult> AddUserToGame(Guid userId)
        {
            // await this.gameManager.AddUserToGameAsync(
            //     Guid.Parse(this.userManager.GetUserId(this.User)));
            await this.gameManager.AddUserToGameAsync(userId);
            return this.Ok();
        }

        [HttpGet]
        [Route("FinishGame")]
        public async Task<IActionResult> FinishGame()
        {
            await this.gameManager.FinishGameAsync(Guid.Empty);
            return this.Ok();
        }

        [HttpPost]
        [Route("MakeStep")]
        public async Task<IActionResult> MakeStep([FromBody] StepDto step, Guid userId)
        {
            var newStep = new Step()
            {
                Time = DateTimeOffset.Now,
                Value = step.Value,
                // UserId = Guid.Parse(this.userManager.GetUserId(this.User)),
                UserId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            };
            await this.gameManager.MakeStepAsync(newStep);
            return this.Ok();
        }
    }
}