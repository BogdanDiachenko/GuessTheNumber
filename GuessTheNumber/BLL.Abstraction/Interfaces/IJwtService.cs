using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Core.Models.Identity;

namespace BLL.Abstraction.Interfaces
{
    public interface IJwtService
    {
        string Generate(ApplicationUser user);

        JwtSecurityToken Verify(string jwt);
    }
}