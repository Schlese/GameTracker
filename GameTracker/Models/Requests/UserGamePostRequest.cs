using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Models.Requests
{
    public class UserGamePostRequest
    {
        public long UserId { get; set; }
        public long GameReleaseId { get; set; }
        public bool isWish { get; set; }
    }
}
