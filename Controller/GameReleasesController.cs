using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameTracker.Data;
using GameTracker.Models;

namespace GameTracker.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameReleasesController : ControllerBase
    {
        private readonly GameTrackerContext _context;

        public GameReleasesController(GameTrackerContext context)
        {
            _context = context;
        }

        // GET: api/GameReleases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameRelease>>> GetGameRelease()
        {
            return await _context.GameRelease
                .Include(release => release.Game)
                .Include(release => release.Platform)
                .ToListAsync();
        }
    }
}
