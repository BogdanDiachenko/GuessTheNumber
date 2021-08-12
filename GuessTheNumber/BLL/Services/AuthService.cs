using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models;
using Core.Models.Identity;
using Core.Models.Responses;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse()
                {
                    Message = "Confirm password doesn't match password.",
                    IsSuccess = false,
                };
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname,
            };

            var result = await this.signInManager.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new UserManagerResponse()
                {
                    Message = "User didn't created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            await this.signInManager.SignInAsync(user, false);
            return new UserManagerResponse()
            {
                Message = "User created successfully",
                IsSuccess = true,
            };
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await this.signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse()
                {
                    Message = "There is no user with that Email.",
                    IsSuccess = false,
                };
            }

            var result = await this.signInManager.PasswordSignInAsync(model.Email, model.Password, false,false);

            if (!result.Succeeded)
            {
                return new UserManagerResponse()
                {
                    IsSuccess = false,
                    Message = "Invalid password."
                };
            }

            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse()
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpirationDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> LogoutUserAsync()
        {
            if (this.signInManager.IsSignedIn(ClaimsPrincipal.Current))
            {
                await this.signInManager.SignOutAsync();

                return new UserManagerResponse()
                {
                    Message = "You've successfully logged out",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse()
            {
                Message = "You are not logged in",
                IsSuccess = false
            };
        }
    }
}