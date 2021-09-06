using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs
{
    public class StepDto
    {
        [Range(long.MinValue, long.MaxValue, ErrorMessage = "The number is too long")]
        public long Value { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}