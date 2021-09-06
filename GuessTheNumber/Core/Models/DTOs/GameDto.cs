using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.DTOs
{
    public class GameDto
    {
        public DateTimeOffset StartTime { get; set; }

        public bool IsFinished { get; set; }

        [Range(long.MinValue, long.MaxValue, ErrorMessage = "The number is too long")]
        public long GuessedNumber { get; set; }

        public Guid HostId { get; set; }

        public Guid? WinnerId { get; set; }

        public List<Guid> PlayersId { get; set; }

        public List<StepDto> Steps { get; set; }
    }
}