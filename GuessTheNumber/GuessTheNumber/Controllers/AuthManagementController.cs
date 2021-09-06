using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models.DTOs;
using Core.Models.Responses;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace GuessTheNumber.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthManagementController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Some properties isn't valid.");
            }

            var result = await this.service.RegisterUserAsync(user);
            if (result.IsSuccess)
            {
                return this.Ok(result);
            }

            return this.BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Unauthorized();
            }

            var result = await this.service.LoginUserAsync(model);

            if (!result.IsSuccess)
            {
                return this.Unauthorized();
            }

            return this.Ok(result.Value);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await this.service.LogoutUserAsync();
            return this.Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var email = this.User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
            {
                return this.BadRequest("User isn't authenticated");
            }

            var result = await this.service.GetCurrentUser(email);
            return this.Ok(result.Value);
        }
    }
}