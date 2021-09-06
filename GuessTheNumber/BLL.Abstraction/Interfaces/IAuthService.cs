using System.Threading.Tasks;
using Core.Models.DTOs;
using Core.Models.Responses;
using Core.Models.ViewModels;

namespace BLL.Abstraction.Interfaces
{
    public interface IAuthService
    {
        Task<Response<UserDto>> RegisterUserAsync(RegisterViewModel model);

        Task<Response<UserDto>> LoginUserAsync(LoginViewModel model);

        Task<Response<UserDto>> LogoutUserAsync();

        Task<Response<UserDto>> GetCurrentUser(string email);
    }
}