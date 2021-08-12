using System;
using System.Collections.Generic;
using Core.Models.Identity;

namespace Core.Models
{
    public class Game : BaseEntity<Guid>
    {
        public bool IsFinished { get; set; }

        public int GuessedNumber { get; set; }

        public Guid WinnerId { get; set; }

        public Guid HostId { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public IList<Step> Steps { get; set; }

        public IList<ApplicationUser> Players { get; set; }
        
        public IList<UserGame> UserGames { get; set; }
    }
}