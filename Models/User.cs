using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameTracker.Models
{
    public class User : People
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<UserGame> UserGames { get; set; }
    }
}
