using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.DTOs;
using Core.Models.ViewModels;
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

        public GameController(IGameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        [HttpPost]
        [Route("StartGame")]
        public IActionResult StartGame([FromBody] GameViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Some properties isn't valid.");
            }

            var result = this.gameManager.StartGame(this.ToDto(model));
            return this.Ok(result);
        }

        [HttpPost]
        [Route("JoinGame")]
        public IActionResult AddUserToGame()
        {
            var currentUserId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = this.gameManager.AddUserToGame(currentUserId);
            return this.Ok(result);
        }

        [HttpGet]
        [Route("FinishGame")]
        public async Task<IActionResult> FinishGame()
        {
            var currentUserId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await this.gameManager.FinishGameAsync(null, currentUserId);
            return this.Ok(result);
        }

        [HttpPost]
        [Route("MakeStep")]
        public async Task<IActionResult> MakeStep([FromBody] StepViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Some properties aren't valid.");
            }

            var currentUserId = Guid.Parse(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await this.gameManager.MakeStepAsync(this.ToDto(model), currentUserId);
            return this.Ok(result);
        }
    }
}