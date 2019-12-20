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
    public class GameReleasesControllerTest : IntegrationTest
    {
        [Fact]
        public async Task A_GetGameReleases_ReturnsReleases()
        {
            var response = await TestClient.GetAsync("api/GameReleases");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonString = await response.Content.ReadAsStringAsync();
            List<GameRelease> releases = JsonConvert.DeserializeObject<List<GameRelease>>(jsonString);
            releases.Count.Should().Be(12);
        }
    }
}
