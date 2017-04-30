using System.Collections.Generic;
using System.Threading.Tasks;

namespace Varus.Stopwatch.Services
{
    public interface IStopwatchService
    {
        Task<Dictionary<string, long>> MapElapsedTimesByNameAsync(string user);
        Task RestartStopwatchAsync(string user, string name);
    }
}
