using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Requests
{
    public class UserLoginRequest
    {
        [Required]
        [MaxLength(2)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}