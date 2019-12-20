using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Models
{
    public class Game
    {
        public long GameId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
    }
}
