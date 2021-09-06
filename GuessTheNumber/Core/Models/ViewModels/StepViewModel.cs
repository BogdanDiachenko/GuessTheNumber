using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class StepViewModel
    {
        [Required]
        [Range(long.MinValue, long.MaxValue, ErrorMessage = "The number is too long")]
        public long Value { get; set; }
    }
    
}