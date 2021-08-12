using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public IList<Game> Games { get; set; }
        
        public IList<UserGame> UserGames { get; set; }
    }
}