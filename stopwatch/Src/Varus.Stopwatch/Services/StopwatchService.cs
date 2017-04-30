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
            _getTimestamp = getTimestamp;
        }

        public async Task<Dictionary<string, long>> MapElapsedTimesByNameAsync(string user)
        {
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
