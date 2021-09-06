using System.IdentityModel.Tokens.Jwt;

namespace Core.Models.DTOs
{
    public class UserDto
    {
        public string Id;
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }
    }
}