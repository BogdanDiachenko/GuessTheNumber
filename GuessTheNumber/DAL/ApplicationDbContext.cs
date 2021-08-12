using System;
using Core.Models;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<ApplicationUser>()
                .HasMany(user => user.Games)
                .WithMany(game => game.Players)
                .UsingEntity<UserGame>(
                    j => j
                        .HasOne(ug => ug.Game)
                        .WithMany(game => game.UserGames)
                        .HasForeignKey(ug => ug.GameId),
                    j => j
                        .HasOne(ug => ug.User)
                        .WithMany(u => u.UserGames)
                        .HasForeignKey(ug => ug.UserId),
                    j =>
                    {
                        j.HasKey(ug => new { ug.GameId, ug.UserId });
                        j.ToTable("UserGames");
                    });
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Step> Steps { get; set; }
        
        public DbSet<UserGame> UserGames { get; set; }
    }
}