using System;
using System.ComponentModel.DataAnnotations;
using Core.Models.BaseEntity;
using Core.Models.Identity;

namespace Core.Models
{
    public class Step : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public DateTimeOffset Time { get; set; }

        [Range(long.MinValue, long.MaxValue, ErrorMessage = "The number is too long") ]
        public long Value { get; set; }

        public int StepNumber { get; set; }
    }
}