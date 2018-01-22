using APIAuth0;
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
using Xunit;

namespace Test
{
    public class ApplicationTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private IConfiguration _configuration;
        public ApplicationTest()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(@"../../.."))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(_configuration));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task UnAuthorizedAccess()
        {
            var response = await _client.GetAsync("/api/books");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        public async Task<string> GetToken()
        {
            var auth0Client = new HttpClient();
            string token = "";
            var bodyString = $@"{{""client_id"":""{_configuration["Auth0:ClientId"]}"", ""client_secret"":""{_configuration["Auth0:ClientSecret"]}"", ""audience"":""{_configuration["Auth0:Audience"]}"", ""grant_type"":""client_credentials""}}";
            var response = await auth0Client.PostAsync($"{_configuration["Auth0:Authority"]}oauth/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JObject.Parse(responseString);
                token = (string)responseJson["access_token"];

            }

            return token;
        }

        [Fact]
        public async Task TestGetToken()
        {
            var auth0Client = new HttpClient();
            var bodyString = $@"{{""client_id"":""{_configuration["Auth0:ClientId"]}"", ""client_secret"":""{_configuration["Auth0:ClientSecret"]}"", ""audience"":""{_configuration["Auth0:Audience"]}"", ""grant_type"":""client_credentials""}}";
            var response = await auth0Client.PostAsync($"{_configuration["Auth0:Authority"]}oauth/token", new StringContent(bodyString, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseString);
            Assert.NotNull((string)responseJson["access_token"]);
            Assert.Equal("Bearer", (string)responseJson["token_type"]);
        }

        [Fact]
        public async Task GetBooks()
        {
            var token = await GetToken();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/books");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var booksResponse = await _client.SendAsync(requestMessage);

            Assert.Equal(HttpStatusCode.OK, booksResponse.StatusCode);

            var bookResponseString = await booksResponse.Content.ReadAsStringAsync();
            var bookResponseJson = JArray.Parse(bookResponseString);
            Assert.Equal(4, bookResponseJson.Count);
        }
    }
}
