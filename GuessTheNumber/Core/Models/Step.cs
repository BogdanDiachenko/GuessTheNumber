using System;

namespace Core.Models
{
    public class Step : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public DateTimeOffset Time { get; set; }

        public int Value { get; set; }
    }
}