using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Models
{
    public class GameRelease
    {
        public long GameReleaseId { get; set; }
        public long GameId { get; set; }
        public Game Game { get; set; }
        public long PlatformId { get; set; }
        public Platform Platform { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
