using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match password.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }
    }
}