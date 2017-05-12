using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Varus.Stopwatch.Repositories;

namespace Varus.Stopwatch.Services
{
    public class StopwatchService : IStopwatchService
    {
        private readonly ITimestampRepository _repository;
        private readonly Func<long> _getTimestamp;

        public StopwatchService(ITimestampRepository repository, Func<long> getTimestamp)
        {
            _repository = repository;

            // Getting a current timestamp is abstracted away from the service to ease testing.
            _getTimestamp = getTimestamp;
        }

        public async Task<Dictionary<string, long>> MapElapsedTimesByNameAsync(string user)
        {
            // Whenever a stopwatch is started/restarted, a timestamp (in ticks) is stored.
            // We subtract stored time from current time and convert to milliseconds.
            return (await _repository.MapTimestampsByNameAsync(user)).ToDictionary(
                x => x.Key,
                x => (_getTimestamp() - x.Value) / TimeSpan.TicksPerMillisecond);
        }

        public async Task RestartStopwatchAsync(string user, string name)
        {
            await _repository.AddOrUpdateTimestampAsync(user, name, _getTimestamp());
        }
    }
}
