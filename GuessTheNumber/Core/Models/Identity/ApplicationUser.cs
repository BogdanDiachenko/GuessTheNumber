using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Models.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MinLength(5)]
        [MaxLength(50)]
        public string Surname { get; set; }

        public IList<Game> HostedGames { get; set; }

        public IList<Game> WonGames { get; set; }

        public IList<Game> PlayedGames { get; set; }
    }
}