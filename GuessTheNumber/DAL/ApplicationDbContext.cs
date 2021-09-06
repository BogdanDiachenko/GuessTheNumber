using System;
using System.Security.Policy;
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
                .Entity<Step>()
                .HasOne<Game>(step => step.Game)
                .WithMany(game => game.Steps)
                .HasForeignKey(step => step.GameId);

            builder
                .Entity<Game>()
                .HasOne<ApplicationUser>(game => game.Winner)
                .WithMany(winner => winner.WonGames)
                .HasForeignKey(game => game.WinnerId);

            builder
                .Entity<Game>()
                .HasOne<ApplicationUser>(game => game.Host)
                .WithMany(winner => winner.HostedGames)
                .HasForeignKey(game => game.HostId);

            builder.Entity<ApplicationUser>().Property(user => user.UserName).HasMaxLength(50);
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Step> Steps { get; set; }
    }
}