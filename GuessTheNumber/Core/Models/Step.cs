using System;

namespace Core.Models
{
    public class Step : IBaseEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public int Value { get; set; }
    }
}