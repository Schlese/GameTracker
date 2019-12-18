using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameTracker.Models;

namespace GameTracker.Data
{
    public class GameTrackerContext : DbContext
    {
        public GameTrackerContext (DbContextOptions<GameTrackerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var pc = new Platform() { PlatformId = 1, Name = "PC" };
            var ps4 = new Platform() { PlatformId = 2, Name = "PS4" };
            var xboxOne = new Platform() { PlatformId = 3, Name = "XBOX One" };
            var nswitch = new Platform() { PlatformId = 4, Name = "Switch" };
            modelBuilder.Entity<Platform>().HasData(pc, ps4, xboxOne, nswitch);

            var borderlands = new Game() { GameId = 1, Title = "Borderlands 3", Genre = "Shooter" };
            var darksiders = new Game() { GameId = 2, Title = "Darksiders 3", Genre = "RPG" };
            var totalWar = new Game() { GameId = 3, Title = "Total War: Three Kingdoms", Genre = "Strategy" };
            var luigi = new Game() { GameId = 4, Title = "Luigi's Mansion 3", Genre = "Adventure" };
            var monsterHunter = new Game() { GameId = 5, Title = "Monster Hunter World", Genre = "Adventure" };
            var kingdomHearts = new Game() { GameId = 6, Title = "Kingdom Hearts 3", Genre = "RPG" };
            modelBuilder.Entity<Game>().HasData(borderlands, darksiders, totalWar, luigi, monsterHunter, kingdomHearts);

            modelBuilder.Entity<GameRelease>().HasData(
                new GameRelease() { GameReleaseId = 1, GameId = 5, PlatformId = 1, ReleaseDate = new DateTime(2019, 6, 23)},
                new GameRelease() { GameReleaseId = 2, GameId = 5, PlatformId = 2, ReleaseDate = new DateTime(2019, 6, 23) },
                new GameRelease() { GameReleaseId = 3, GameId = 5, PlatformId = 3, ReleaseDate = new DateTime(2019, 6, 23) },
                new GameRelease() { GameReleaseId = 4, GameId = 1, PlatformId = 1, ReleaseDate = new DateTime(2019, 9, 15) },
                new GameRelease() { GameReleaseId = 5, GameId = 1, PlatformId = 2, ReleaseDate = new DateTime(2019, 9, 23) },
                new GameRelease() { GameReleaseId = 6, GameId = 1, PlatformId = 3, ReleaseDate = new DateTime(2019, 9, 23) },
                new GameRelease() { GameReleaseId = 7, GameId = 2, PlatformId = 2, ReleaseDate = new DateTime(2019, 2, 3) },
                new GameRelease() { GameReleaseId = 8, GameId = 2, PlatformId = 3, ReleaseDate = new DateTime(2019, 2, 3) },
                new GameRelease() { GameReleaseId = 9, GameId = 3, PlatformId = 1, ReleaseDate = new DateTime(2019, 7, 20) },
                new GameRelease() { GameReleaseId = 10, GameId = 4, PlatformId = 4, ReleaseDate = new DateTime(2019, 10, 10) },
                new GameRelease() { GameReleaseId = 11, GameId = 6, PlatformId = 2, ReleaseDate = new DateTime(2018, 11, 15) });

        }

        public DbSet<GameTracker.Models.People> People { get; set; }
        public DbSet<GameTracker.Models.User> User { get; set; }
        public DbSet<GameTracker.Models.Game> Game { get; set; }
        public DbSet<GameTracker.Models.Platform> Platform { get; set; }
        public DbSet<GameTracker.Models.GameRelease> GameRelease { get; set; }
        public DbSet<GameTracker.Models.UserGame> UserGame { get; set; }
    }
}
