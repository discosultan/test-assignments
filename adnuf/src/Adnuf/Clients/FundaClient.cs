using ComposableAsync;
using Microsoft.Extensions.Logging;
using RateLimiter;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Adnuf.Clients
{
    public class FundaClient
    {
        static readonly string BaseUri = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json";

        readonly HttpClient client;
        readonly TimeLimiter rateLimiter;
        readonly ILogger<FundaClient> logger;

        public FundaClient(HttpClient client, TimeLimiter rateLimiter, ILogger<FundaClient> logger)
        {
            this.client = client;
            this.rateLimiter = rateLimiter;
            this.logger = logger;
        }

        public async Task<FundaResponse<T>> Get<T>(
            string type, string search, int page, int pageSize)
        {
            if (page < 1) throw new ArgumentException("Minimum page is 1.");
            if (pageSize > 25) throw new ArgumentException("Maximum page size is 25.");

            await rateLimiter;

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["type"] = type;
            query["zo"] = search;
            query["page"] = page.ToString();
            query["pageSize"] = pageSize.ToString();

            HttpResponseMessage response;
            while (true)
            {
                response = await client
                    .GetAsync($"?{query}")
                    .ConfigureAwait(false);
                // In case we hit the request limit, we wait and retry infinitely.
                if (response.StatusCode == HttpStatusCode.Unauthorized
                    && response.ReasonPhrase == "Request limit exceeded")
                {
                    const int WaitMinutes = 1;
                    logger.LogWarning(
                        $"Request limit exceeded. Waiting for {WaitMinutes} minute(s) before retrying ...");
                    await Task.Delay(TimeSpan.FromMinutes(WaitMinutes)).ConfigureAwait(false);
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    break;
                }
            }
            var content = await response.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);
            var data = await JsonSerializer
                .DeserializeAsync<FundaResponse<T>>(content)
                .ConfigureAwait(false);

            return data;
        }

        public static Uri ConstructBaseUri(string token) => new Uri($"{BaseUri}/{token}/");

        // TODO: Use a rate limiter based on a token bucket strategy. The current one is based on
        // a fixed window, meaning that we need to wait for the entire duration before new calls
        // are released.
        public static TimeLimiter ConstructRateLimiter() => TimeLimiter.GetFromMaxCountByInterval(
            100, TimeSpan.FromMinutes(1));
    }

    public class FundaResponse<T>
    {
        public List<T> Objects { get; set; }
        public FundaPaging Paging { get; set; }
    }

    public class FundaPaging
    {
        [JsonPropertyName("AantalPaginas")]
        public int Pages { get; set; }

        [JsonPropertyName("HuidigePagina")]
        public int CurrentPage { get; set; }
    }
}
