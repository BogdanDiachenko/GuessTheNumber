using System.Threading.Tasks;
using Core.Models.Responses;
using Core.Models.ViewModels;

namespace BLL.Abstraction.Interfaces
{
    public interface IAuthService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> LogoutUserAsync();
    }
}