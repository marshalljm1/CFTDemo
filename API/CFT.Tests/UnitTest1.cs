using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using CFT.API;
using CFT.API.Models;
using Newtonsoft.Json;
using Xunit;

namespace CFT.Tests
{
    public class ApiTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public ApiTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../.."))
                .AddJsonFile("appsettings.json", optional: false)
                //.AddUserSecrets<Startup>()
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();
        }

        
        [Fact]
        public async void GetSingleTripNotNull()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Trips/8");
            var response = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Trips>(responseString);
            Assert.NotNull(obj);
        }

        [Fact]
        public async void GetSingleBusNotNull()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Bus/1");
            var response = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Trips>(responseString);
            Assert.NotNull(obj);
        }

        [Fact]
        public async void GetTripsByBusNotNull()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Trips/GetTripsByBus/1");
            var response = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JArray.Parse(responseString);
            Assert.True(responseJson.Count > 0);
        }

        [Fact]
        public async void GetMessageNotNull()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Messages/");
            var response = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonObj = JsonConvert.DeserializeObject<Messages>(responseString);
            Assert.NotNull(jsonObj);
        }
    }
}
