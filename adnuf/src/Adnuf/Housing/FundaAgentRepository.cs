using Adnuf.Clients;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnuf.Housing
{
    public class FundaAgentRepository : IAgentRepository
    {
        // Translations from English to Dutch parameters. If a translation is missing, the original
        // parameter is passed as-is.
        static readonly Dictionary<string, string> Extras = new Dictionary<string, string>(
            2, StringComparer.InvariantCultureIgnoreCase)
        {
            ["garden"] = "tuin",
            ["boathouse"] = "woonboot",
            // ...
        };

        readonly FundaClient client;
        readonly ILogger<FundaAgentRepository> logger;

        public FundaAgentRepository(FundaClient client, ILogger<FundaAgentRepository> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<List<Agent>> ListTopAgentsByProperties(
            string city,
            params string[] extras)
        {
            // Because we don't know whether Funda API supports aggregating and ordering by agent,
            // we will fetch all the available data. Since we don't know the number of pages of
            // available data, we start by fetching the first page to figure that out. Then we
            // proceed fetching rest of the data in a concurrent manner.
            const string SearchType = "koop";
            const int PageSize = 25;  // Seems to be the max.

            var searchQueryBuilder = new StringBuilder();
            searchQueryBuilder.Append("/");
            searchQueryBuilder.Append(city);
            foreach (string extra in extras)
            {
                string mappedExtra = extra;
                Extras.TryGetValue(mappedExtra, out mappedExtra);
                searchQueryBuilder.Append("/");
                searchQueryBuilder.Append(mappedExtra);
            }
            string searchQuery = searchQueryBuilder.ToString();

            logger.LogInformation(
                $"Fetching first page of size {PageSize} to determine total pages.");
            FundaResponse<Property> firstPage = await client
                .Get<Property>(SearchType, searchQuery, 1, PageSize)
                .ConfigureAwait(false);

            logger.LogInformation($"Fetching rest of the {firstPage.Paging.Pages} total pages.");
            var tasks = new List<Task<FundaResponse<Property>>>(firstPage.Paging.Pages - 1);
            for (int i = 2; i <= firstPage.Paging.Pages; i++)
            {
                tasks.Add(client.Get<Property>(SearchType, searchQuery, i, PageSize));
            }
            var subsequentPages = await Task.WhenAll(tasks).ConfigureAwait(false);

            return firstPage.Objects
                // Concat first page with subsequent pages.
                .Concat(subsequentPages.SelectMany(r => r.Objects))
                // Group by agent.
                .GroupBy(p => p.AgentId)
                // Aggregate properties per agent.
                .Select(g => new Agent
                {
                    Id = g.Key,
                    Name = g.First().AgentName,
                    PropertyCount = g.Count(),
                })
                // Put agents with most properties top.
                .OrderByDescending(a => a.PropertyCount)
                .ToList();
        }
    }
}
