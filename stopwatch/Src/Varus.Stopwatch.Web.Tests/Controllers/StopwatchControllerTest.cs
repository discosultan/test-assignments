using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Xunit;

namespace Varus.Stopwatch.Web.Tests.Controllers
{
    [Collection("stopwatch")]
    public class StopwatchControllerTest : IClassFixture<StopwatchControllerTest.Context>
    {
        [Fact]
        public async Task GetAsync_UnauthorizedWithoutAuth()
        {
            var response = await _server.CreateRequest("/api/stopwatch/testuser")
                .GetAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_OKWithBasicAuth()
        {
            var response = await _server.CreateRequest("/api/stopwatch/testuser")
                .AddHeader("Authorization", BasicAuth)
                .GetAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_OKWithAPIKeyAuth()
        {
            var response = await _server.CreateRequest("/api/stopwatch/testuser")
                .AddHeader("Authorization", APIKeyAuth)
                .GetAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task PostAsync_UnauthorizedWithoutAuth()
        {
            var response = await _server.CreateRequest("/api/stopwatch")
                .PostAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostAsync_UnauthorizedWithAPIKeyAuth()
        {
            var response = await _server.CreateRequest("/api/stopwatch")
                .AddHeader("Authorization", APIKeyAuth)
                .PostAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostAsync_BadRequestWithoutNameInBody()
        {
            var response = await _server.CreateRequest("/api/stopwatch")
                .AddHeader("Authorization", BasicAuth)
                .PostAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostAsync_OKWithBasicAuth()
        {
            var response = await _server.CreateRequest("/api/stopwatch")
                .AddHeader("Authorization", BasicAuth)
                .And(msg => msg.Content = new StringContent(
                    "{ \"name\" : \"testname\" }",
                    Encoding.UTF8,
                    "application/json"))
                .PostAsync();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact]
        public async Task StartStopwatchAndGetElapsedTime()
        {
            await _server.CreateRequest("/api/stopwatch")
                .AddHeader("Authorization", BasicAuth)
                .And(msg => msg.Content = new StringContent(
                    "{ \"name\" : \"testname\" }",
                    Encoding.UTF8,
                    "application/json"))
                .PostAsync();

            var response = await _server.CreateRequest("/api/stopwatch/testuser")
                .AddHeader("Authorization", BasicAuth)
                .GetAsync();

            var body = await response.Content.ReadAsStringAsync();
            Assert.Equal("{\"testname\":1}", body);
        }

        private const string BasicAuth = "Basic dGVzdHVzZXI6dGVzdHVzZXI="; // testuser:testuser
        private const string APIKeyAuth = "API-Key test";

        private readonly TestServer _server;

        public StopwatchControllerTest(Context ctx) => _server = ctx.Server;

        public class Context : IDisposable
        {
            public TestServer Server { get; } = TestServer.Create<TestsStartup>();
            public void Dispose() => Server.Dispose();
        }
    }
}
