using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Game : BaseEntity<Guid>
    {
        public bool HasFinishedSuccesfully { get; set; }

        public int GuessedNumber { get; set; }

        public int PlayersCount { get; set; }

        public Guid? WinnerId { get; set; }

        public Guid HostId { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public IEnumerable<Step> Steps { get; set; }

        public IEnumerable<ApplicationUser> Players { get; set; }
    }
}