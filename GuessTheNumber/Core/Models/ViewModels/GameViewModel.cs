using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class GameViewModel
    {
        [Required]
        [Range(long.MinValue, long.MaxValue, ErrorMessage = "The number is too long")]
        public long GuessedNumber { get; set; }
    }
}