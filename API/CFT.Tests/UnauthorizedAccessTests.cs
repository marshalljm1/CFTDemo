using FruitionAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FruitionAPI.Models;
using Newtonsoft.Json;
using Xunit;

namespace Test
{
    public class UnauthorizedAccessTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public UnauthorizedAccessTests()
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
        public async Task SchoolUnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/school");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task SchoolSystemUnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/schoolsystem");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CountyUnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/county");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task RegionUnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/region");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task StateUnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/state");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

    public class GetTokenTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public GetTokenTests()
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
        public async Task GetToken()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            Assert.NotNull((string)responseJson["token"]);
        }
    }

    public class GetSingleDataTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public GetSingleDataTests()
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
        public async Task GetSingleStateReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/state/2");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolObject = JsonConvert.DeserializeObject<Schools>(schoolsResponseString);
            Assert.NotNull(schoolObject);
        }

        [Fact]
        public async Task GetSingleRegionReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/region/2");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolObject = JsonConvert.DeserializeObject<Schools>(schoolsResponseString);
            Assert.NotNull(schoolObject);
        }

        [Fact]
        public async Task GetSingleCountyReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/county/2");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolObject = JsonConvert.DeserializeObject<Schools>(schoolsResponseString);
            Assert.NotNull(schoolObject);
        }

        [Fact]
        public async Task GetSingleSchoolSystemReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/schoolsystem/2");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolObject = JsonConvert.DeserializeObject<SchoolSystems>(schoolsResponseString);
            Assert.NotNull(schoolObject);
        }

        [Fact]
        public async Task GetSingleSchoolReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/school/1");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolObject = JsonConvert.DeserializeObject<Schools>(schoolsResponseString);
            Assert.NotNull(schoolObject);
        }


    }

    public class GetAllDataTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public GetAllDataTests()
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
        public async Task GetAllStatesReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/state");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }

        [Fact]
        public async Task GetAllRegionsReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/region");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }

        [Fact]
        public async Task GetAllCountiesReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/county");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }

        [Fact]
        public async Task GetAllSchoolSystemsReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/schoolsystem");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }

        [Fact]
        public async Task GetAllSchoolsReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/school");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }
    }

    public class GetSubsetDataTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public GetSubsetDataTests()
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
        public async Task GetAllCountiesByStateReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/County/GetCountiesByState/8");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }

        [Fact]
        public async Task GetAllSystemsByCountyReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/schoolsystem/getsystemsbycounty/28");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }

        [Fact]
        public async Task GetAllSchoolsBySystemReturnsNotNull()
        {
            var bodyString = @"{username: ""jmarshall"", password: ""Password01""}";
            var response = await _client.PostAsync("/api/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            var token = (string)responseJson["token"];

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/school/GetSchoolsBySystem/3");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var schoolsResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, schoolsResponse.StatusCode);

            var schoolsResponseString = await schoolsResponse.Content.ReadAsStringAsync();
            var schoolsResponseJson = JArray.Parse(schoolsResponseString);
            Assert.Equal(true, schoolsResponseJson.Count > 0);
        }
    }
}
