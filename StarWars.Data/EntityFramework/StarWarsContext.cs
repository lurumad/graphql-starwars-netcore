using Microsoft.EntityFrameworkCore;
using StarWars.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data.EntityFramework
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterFriend> CharacterFriends { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public DbSet<Droid> Droids { get; set; }
        public DbSet<Human> Humans { get; set; }

        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Episode>().HasKey(c => c.Id);
            modelBuilder.Entity<Episode>().Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.Entity<Planet>().HasKey(c => c.Id);
            modelBuilder.Entity<Planet>().Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.Entity<Character>().HasKey(c => c.Id);
            modelBuilder.Entity<Character>().Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.Entity<CharacterFriend>().HasKey(t => new { t.CharacterId, t.FriendId });

            modelBuilder.Entity<CharacterFriend>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.CharacterFriends)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterFriend>()
                .HasOne(cf => cf.Friend)
                .WithMany(t => t.FriendCharacters)
                .HasForeignKey(cf => cf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterEpisode>().HasKey(t => new { t.CharacterId, t.EpisodeId });

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(cf => cf.Character)
                .WithMany(c => c.CharacterEpisodes)
                .HasForeignKey(cf => cf.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterEpisode>()
                .HasOne(cf => cf.Episode)
                .WithMany()
                .HasForeignKey(cf => cf.EpisodeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Human>().HasOne(h => h.HomePlanet).WithMany(p => p.Humans);
        }
    }
}
