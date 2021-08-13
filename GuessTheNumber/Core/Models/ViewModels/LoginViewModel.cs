using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}