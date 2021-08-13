using System;
using System.Collections.Generic;

namespace Core.Models.DTOs
{
    public class GameDto
    {
        public DateTimeOffset StartTime { get; set; }

        public bool IsFinished { get; set; }

        public int GuessedNumber { get; set; }

        public Guid HostId { get; set; }

        public Guid WinnerId { get; set; }

        public List<Guid> PlayersId { get; set; }

        public List<StepDto> Steps { get; set; }
    }
}