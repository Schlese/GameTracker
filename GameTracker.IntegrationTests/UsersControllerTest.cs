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

namespace GameTracker.IntegrationTests
{
    public class UsersControllerTest : IntegrationTest
    {
        private readonly string AllUsersUri = "/api/Users";

        [Fact]
        public async Task A_GetUser_ReturnsEmptyResponse()
        {
            var response = await TestClient.GetAsync(AllUsersUri);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task B_PostUser_ReturnsNewUser()
        {
            User user = new User() { FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };

            var response = await TestClient.PostAsJsonAsync(AllUsersUri, user);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var jsonString = await response.Content.ReadAsStringAsync();
            User createdUser = JsonConvert.DeserializeObject<User>(jsonString);
            createdUser.Username.Should().Be("Schlese");
        }

        [Fact]
        public async Task C_GetUser_ReturnsNewUser()
        {
            User newUser = new User() { PeopleId = 25, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var response = await TestClient.GetAsync("api/Users/25");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(jsonString);
            user.Username.Should().Be("Schlese");
        }

        [Fact]
        public async Task D_DeleteUser_ReturnsOk()
        {
            User newUser = new User() { PeopleId = 99, FirstName = "Niklas", LastName = "Thier", Email = "niklas@thier.at", Username = "Schlese" };
            await TestClient.PostAsJsonAsync(AllUsersUri, newUser);

            var response = await TestClient.DeleteAsync("api/Users/99");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
