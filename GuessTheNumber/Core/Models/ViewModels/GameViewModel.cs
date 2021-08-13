using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.ViewModels
{
    public class GameViewModel
    {
        [Required]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "The number is too long"),]
        public int GuessedNumber { get; set; }
    }
}