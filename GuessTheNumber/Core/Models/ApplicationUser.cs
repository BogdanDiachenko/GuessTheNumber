using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IEnumerable<Game> GamesHistory { get; set; }
    }
}