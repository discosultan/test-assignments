
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adnuf.Housing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Tababular;

namespace Adnuf.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly IAgentRepository repository;
        private readonly IMemoryCache cache;

        public AgentController(IAgentRepository repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        [HttpGet]
        [Route("top_by_properties")]
        public async Task<string> GetTopByProperties(
            [FromQuery]string city = "amsterdam",
            [FromQuery]int limit = 10,
            [FromQuery]string[]? extras = null)
        {
            if (extras == null) extras = new string[0];

            // We are fetching a lot of duplicate entries because the second query
            // (properties with garden) will return a subset of objects already available in the
            // initial query. However, becuase the property entity does not contain info whether
            // it has a garden or not, we need to make fetch them separately (instead of doing
            // client side filtering)

            // Since fetching this from Funda requires walking over all the available data, we
            // cache the results based on `city` and `extras`. We don't cache based on `limit`
            // because we need to fetch all the properties anyway regardless of client requested
            // limit.
            var cacheKey = (city, extras);
            if (!cache.TryGetValue(cacheKey, out List<Agent> agents))
            {
                agents = await repository.ListTopAgentsByProperties(city, extras);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));
                cache.Set(cacheKey, agents, cacheEntryOptions);
            }

            var tableFormatter = new TableFormatter();
            return tableFormatter.FormatObjects(agents.Take(limit));
            //return agents.Take(limit);
        }
    }
}
