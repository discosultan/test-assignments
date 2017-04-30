using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Varus.Stopwatch.Services;
using Varus.Stopwatch.Web.Infrastructure.WebAPI;

namespace Varus.Stopwatch.Web.Controllers
{
    public class StopwatchController : ApiController
    {
        private readonly IStopwatchService _service;

        public StopwatchController(IStopwatchService service)
        {
            _service = service;
        }

        [Route("api/stopwatch/{user}")]
        [AuthorizeClaim("Basic", "API-Key")]
        public async Task<Dictionary<string, long>> GetAsync(string user)
        {
            return await _service.MapElapsedTimesByNameAsync(user);
        }

        [Route("api/stopwatch")]
        [AuthorizeClaim("Basic")]
        public async Task PostAsync([FromClaim]string user, RestartStopwatch model)
        {
            if (string.IsNullOrWhiteSpace(model?.Name))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = $"Missing {nameof(model.Name)} parameter from request body."
                });
            }
            await _service.RestartStopwatchAsync(user, model.Name);
        }
    }

    public class RestartStopwatch
    {
        public string Name { get; set; }
    }
}