using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs.Requests
{
    public class GameDto
    {
        [Required]
        public int GuessedNumber { get; set; }
    }
}