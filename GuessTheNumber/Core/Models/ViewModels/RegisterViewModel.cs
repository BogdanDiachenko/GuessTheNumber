using System.ComponentModel.DataAnnotations;

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
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }
    }
}