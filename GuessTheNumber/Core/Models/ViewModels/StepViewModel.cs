using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class StepViewModel
    {
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "The number is too long"),]
        public int Value { get; set; }
    }
}