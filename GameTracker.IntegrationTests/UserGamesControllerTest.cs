using FluentAssertions;
using GameTracker.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using System;
using Xunit.Extensions.Ordering;
using GameTracker.Models.Requests;

namespace GameTracker.IntegrationTests
{
    public class UserGamesControllerTest : IntegrationTest
    {
        private readonly string AllUserGamesUri = "api/UserGames";
        private readonly string AllUsersUri = "/api/Users";

        [Fact]
        public async Task PostUser_THEN_PostUserGame_THEN_GetWhishlist_ReturnsUserGame()
        {
            User newUser = new User() { PeopleId = 201, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var userGame = new UserGame() { UserGameId = 1, GameReleaseId = 1, UserId = 201, IsWish = true };
            await TestClient.PostAsJsonAsync(AllUserGamesUri, userGame);

            var response = await TestClient.GetAsync(AllUserGamesUri + "/201?list=wishlist");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            List<UserGame> userGames = JsonConvert.DeserializeObject<List<UserGame>>(jsonString);
            userGames.Count.Should().Be(1);
        }

        [Fact]
        public async Task PostUser_THEN_PostUserGame_THEN_GetBacklog_ReturnsEmpty()
        {
            User newUser = new User() { PeopleId = 202, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var userGame = new UserGame() { UserGameId = 1, GameReleaseId = 1, UserId = 202, IsWish = true };
            await TestClient.PostAsJsonAsync(AllUserGamesUri, userGame);

            var response = await TestClient.GetAsync(AllUserGamesUri + "/202?list=backlog");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            List<UserGame> userGames = JsonConvert.DeserializeObject<List<UserGame>>(jsonString);
            userGames.Should().BeEmpty();
        }

        [Fact]
        public async Task PostUser_THEN_PostUserGame_THEN_PutUserGame_THEN_GetBacklog_ReturnsUserGame()
        {
            User newUser = new User() { PeopleId = 203, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var userGame = new UserGame() { UserGameId = 1, GameReleaseId = 1, UserId = 203, IsWish = true };
            await TestClient.PostAsJsonAsync(AllUserGamesUri, userGame);

            var response1 = await TestClient.GetAsync(AllUserGamesUri + "/203?list=wishlist");

            //Assert
            response1.StatusCode.Should().Be(HttpStatusCode.OK);

            var js = await response1.Content.ReadAsStringAsync();
            List<UserGame> games = JsonConvert.DeserializeObject<List<UserGame>>(js);
            games.Count.Should().Be(1);

            var putRequest = new UserGamePutRequest() { IsWish = false };
            await TestClient.PutAsJsonAsync(AllUserGamesUri + "/1", putRequest);

            var response2 = await TestClient.GetAsync(AllUserGamesUri + "/203?list=backlog");

            //Assert
            response2.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonString = await response2.Content.ReadAsStringAsync();
            List<UserGame> userGames = JsonConvert.DeserializeObject<List<UserGame>>(jsonString);
            userGames.Count.Should().Be(1);
        }

        [Fact]
        public async Task PostUser_THEN_PostUserGame_ReturnsBadRequest()
        {
            User newUser = new User() { PeopleId = 204, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var userGame = new UserGame() { UserGameId = 1, GameReleaseId = 12, UserId = 204, IsWish = false };
            var response = await TestClient.PostAsJsonAsync(AllUserGamesUri, userGame);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task PostUser_THEN_PostUserGame_ReturnsUserGame()
        {
            User newUser = new User() { PeopleId = 205, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var userGame = new UserGame() { UserGameId = 1, GameReleaseId = 12, UserId = 205, IsWish = true };
            var response = await TestClient.PostAsJsonAsync(AllUserGamesUri, userGame);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
