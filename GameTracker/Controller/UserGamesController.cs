using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameTracker.Data;
using GameTracker.Helper;
using GameTracker.Models;
using GameTracker.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGamesController : ControllerBase
    {

        private readonly GameTrackerContext _context;

        public UserGamesController(GameTrackerContext context)
        {
            _context = context;
        }

        // GET: api/UserGames/5?list=whishlist
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserGame>>> GetUserGames([FromQuery(Name = "list")] string list, long id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            switch (list)
            {
                case "backlog": return await GetBacklogGames(user);
                default: return await GetWishlistGames(user);
            }
        }

        // PUT: api/UserGames/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserGame>> PutUser(long id, UserGamePutRequest request)
        {
            var userGame = await _context.UserGame.FindAsync(id);

            if (userGame == null)
            {
                return NotFound();
            }

            userGame.IsWish = request.IsWish;
            await _context.SaveChangesAsync();

            return userGame;
        }

        // POST: api/UserGames
        [HttpPost]
        public async Task<ActionResult<UserGame>> PostUser(UserGamePostRequest request)
        {
            var gameRelease = await _context.GameRelease.FindAsync(request.GameReleaseId);

            if (gameRelease == null)
                return NotFound();

            if (!request.isWish && !DateHelper.getInstance().checkBeforeEqualsToday(gameRelease.ReleaseDate))
                return BadRequest("Game is not yet released");

            var userGame = new UserGame() { GameReleaseId = request.GameReleaseId, UserId = request.UserId, IsWish = request.isWish };
            _context.UserGame.Add(userGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserGames", new { id = userGame.UserGameId }, userGame);
        }

        // DELETE: api/UserGames/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserGame>> DeleteUserGame(long id)
        {
            var userGame = await _context.UserGame.FindAsync(id);
            if (userGame == null)
            {
                return NotFound();
            }

            _context.UserGame.Remove(userGame);
            await _context.SaveChangesAsync();

            return userGame;
        }

        private async Task<List<UserGame>> GetWishlistGames(User user)
        {
            return await _context.Entry(user)
                .Collection(u => u.UserGames)
                .Query()
                .Where(ug => ug.IsWish == true)
                .Include(ug => ug.GameRelease)
                    .ThenInclude(release => release.Game)
                .Include(ug => ug.GameRelease)
                    .ThenInclude(release => release.Platform)
                .ToListAsync();
        }

        private async Task<List<UserGame>> GetBacklogGames(User user)
        {
            return await _context.Entry(user)
                .Collection(u => u.UserGames)
                .Query()
                .Where(ug => ug.IsWish == false)
                .Include(ug => ug.GameRelease)
                    .ThenInclude(release => release.Game)
                .Include(ug => ug.GameRelease)
                    .ThenInclude(release => release.Platform)
                .ToListAsync();
        }

        private bool UserGameExists(long id)
        {
            return _context.UserGame.Any(e => e.UserGameId == id);
        }
    }
}