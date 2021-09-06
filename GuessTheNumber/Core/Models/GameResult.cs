using System;
using System.Collections.Generic;
using Core.Models.Identity;
using Core.Models.ViewModels;

namespace Core.Models
{
    public class GameResult
    {
        public Guid GameId { get; set; }

        public UserViewModel Winner { get; set; }

        public UserViewModel Host { get; set; }

        public int PlayersCount { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public long GuessedNumber { get; set; }
    }
}