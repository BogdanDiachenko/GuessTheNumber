using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs.Requests
{
    public class StepDto
    {
        [Required]
        public int Value { get; set; }
    }
}