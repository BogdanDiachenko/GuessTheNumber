using System;
using Core.Models.Identity;

namespace Core.Models
{
    public class UserGame
    {
        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}