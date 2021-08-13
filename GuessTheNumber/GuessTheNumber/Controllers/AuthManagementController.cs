using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthManagementController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("Register")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Some properties are not valid");
            }

            var result = await this.service.LoginUserAsync(model);

            if (!result.IsSuccess)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await this.service.LogoutUserAsync();
            if (!result.IsSuccess)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }
    }
}