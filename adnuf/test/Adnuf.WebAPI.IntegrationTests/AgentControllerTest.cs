using Adnuf.Clients;
using Adnuf.WebAPI;
using Adnuf.WebAPI.IntegrationTests.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Adnuf.UnitTests
{

    public class AgentControllerTest
    {
        [Fact]
        public async Task GetTopByProperties_HappyFlow()
        {
            // Mock responses from Funda.
            var mockedResponses = await Task.WhenAll(
                File.ReadAllTextAsync("Data/FundaProperties1.json"),
                File.ReadAllTextAsync("Data/FundaProperties2.json"));

            var builder = new WebHostBuilder()
                .UseSetting("token", "dummy")
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {
                    services.AddHttpClient<FundaClient>()
                        .AddHttpMessageHandler(() => new MockHttpMessageHandler(mockedResponses));
                });

            using var server = new TestServer(builder);

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["city"] = "amsterdam";
            query["extras"] = "garden";
            query["limit"] = "10";
            var response = await server
                .CreateRequest($"/agent/top_by_properties?{query}")
                .GetAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
