using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Varus.Stopwatch.Repositories
{
    public class InMemoryTimestampRepository : ITimestampRepository
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, long>> _db =
            new ConcurrentDictionary<string, ConcurrentDictionary<string, long>>();

        public Task AddOrUpdateTimestampAsync(string user, string name, long timestamp)
        {
            IDictionary<string, long> timestamps = GetOrAddUserTimestamps(user);
            timestamps[name] = timestamp;
            return Task.FromResult<object>(null);
        }

        public Task<IDictionary<string, long>> MapTimestampsByNameAsync(string user)
        {
            return Task.FromResult(GetOrAddUserTimestamps(user));
        }

        private IDictionary<string, long> GetOrAddUserTimestamps(string user)
        {
            if (!_db.TryGetValue(user, out ConcurrentDictionary<string, long> timestamps))
            {
                timestamps = new ConcurrentDictionary<string, long>();
                timestamps = _db.GetOrAdd(user, timestamps);
            }
            return timestamps;
        }
    }
}
