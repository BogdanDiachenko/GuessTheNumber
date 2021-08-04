using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public IEnumerable<Game> GamesHistory { get; set; }
    }
}