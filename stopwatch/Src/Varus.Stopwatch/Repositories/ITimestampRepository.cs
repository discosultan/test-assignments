using System.Collections.Generic;
using System.Threading.Tasks;

namespace Varus.Stopwatch.Repositories
{
    public interface ITimestampRepository
    {
        Task AddOrUpdateTimestampAsync(string user, string name, long timestamp);
        Task<IDictionary<string, long>> MapTimestampsByNameAsync(string user);
    }
}
