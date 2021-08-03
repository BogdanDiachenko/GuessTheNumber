using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}