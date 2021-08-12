using System;

namespace Core.Models.DTOs
{
    public class StepDto
    {
        public int Value { get; set; }

        public Guid UserId { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}