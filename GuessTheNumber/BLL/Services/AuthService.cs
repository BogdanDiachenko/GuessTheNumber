using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstraction.Interfaces;
using Core.Models.DTOs;
using Core.Models.Identity;
using Core.Models.Responses;
using Core.Models.ViewModels;
using Core.Options;
using DAL.Abstraction.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{

    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IJwtService jwtService;
        
        public AuthService(SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }

        public async Task<Response<UserDto>> RegisterUserAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.Surname,
            };

            var result = await this.signInManager.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return Response<UserDto>.Failure(string.Join(", ", result.Errors.Select(e => e.Description).ToList()));
            }

            return await this.LoginUserAsync(new LoginViewModel()
            {
                Email = model.Email,
                Password = model.Password
            });
        }

        public async Task<Response<UserDto>> LoginUserAsync(LoginViewModel model)
        {
            var user = await this.signInManager.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Response<UserDto>.Failure("There is no users with that email");
            }

            var result = await this.signInManager.PasswordSignInAsync(
                await this.signInManager.UserManager.FindByEmailAsync(model.Email), model.Password, false, false);

            if (!result.Succeeded)
            {
                return Response<UserDto>.Failure("Invalid password");
            }

            string token = await Task.Run(() => token = this.jwtService.Generate(user));

            return Response<UserDto>.Success(this.ToDto(user, token));
        }

        public async Task<Response<UserDto>> LogoutUserAsync()
        {
            await this.signInManager.SignOutAsync();

            return Response<UserDto>.Success(null);
        }

        public async Task<Response<UserDto>> GetCurrentUser(string email)
        {
            ApplicationUser user = await this.signInManager.UserManager.FindByEmailAsync(email);

            return Response<UserDto>.Success(this.ToDto(user));
        }

        private UserDto ToDto(ApplicationUser user)
        {
            return new()
            {
                UserName = user.UserName,
                Token = this.jwtService.Generate(user),
                Email = user.Email
            };
        }

        private UserDto ToDto(ApplicationUser user, string token)
        {
            return new()
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                Token = token,
                Email = user.Email
            };
        }
        
        
        
    }
}