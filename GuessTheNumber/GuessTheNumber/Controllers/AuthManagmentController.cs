using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs.Requests;
using Core.Models;
using GuessTheNumber.Configuration;
using GuessTheNumber.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuessTheNumber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly JwtConfig jwtConfig;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthManagementController(
            IOptionsMonitor<JwtConfig> optionsMonitor,
            UserManager<ApplicationUser> userManager)
        {
            this.jwtConfig = optionsMonitor.CurrentValue;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegestrationDto user)
        {
            if (this.ModelState.IsValid)
            {
                var existingUser = await this.userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return this.BadRequest(new RegistrationResponse
                    {
                        Errors = new List<string>
                        {
                            "Email already in use."
                        },
                        Success = false
                    });
                }

                var newUser = new ApplicationUser
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname
                };
                var isCreated = await this.userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = this.GenerateJwtToken(newUser);
                    return this.Ok(new RegistrationResponse
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }

                return this.BadRequest(new RegistrationResponse
                {
                    Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                    Success = false
                });
            }

            return this.BadRequest(new RegistrationResponse
            {
                Errors = new List<string>
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (this.ModelState.IsValid)
            {
                var existingUser = await this.userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return this.BadRequest(new RegistrationResponse
                    {
                        Errors = new List<string>
                        {
                            "Invalid login request."
                        },
                        Success = false
                    });
                }

                var isCorrect = await this.userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    return this.BadRequest(new RegistrationResponse
                    {
                        Errors = new List<string>
                        {
                            "Invalid login request."
                        },
                        Success = false
                    });
                }

                var jwtToken = this.GenerateJwtToken(existingUser);
                return this.Ok(new RegistrationResponse
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return this.BadRequest(new RegistrationResponse
            {
                Errors = new List<string>
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}