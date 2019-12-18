using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameTracker.Models
{
    public class UserGame
    {
        public long UserGameId { get; set; }

        public long UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public long GameReleaseId { get; set; }
        public GameRelease GameRelease { get; set; }

        public bool IsWish { get; set; }
    }
}
