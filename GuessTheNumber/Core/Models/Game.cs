using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Models.BaseEntity;
using Core.Models.Identity;

namespace Core.Models
{
    public class Game : BaseEntity<Guid>
    {
        public bool IsFinished { get; set; }

        [Range(long.MinValue, long.MaxValue, ErrorMessage = "The number is too long")]
        public long GuessedNumber { get; set; }

        public Guid? WinnerId { get; set; }

        public ApplicationUser Winner { get; set; }

        public Guid HostId { get; set; }

        public ApplicationUser Host { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public int PlayersCount { get; set; }

        public IList<Step> Steps { get; set; }
    }
}