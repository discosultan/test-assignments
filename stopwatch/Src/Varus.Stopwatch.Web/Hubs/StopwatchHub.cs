using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Varus.Stopwatch.Services;
using Varus.Stopwatch.Web.Infrastructure.SignalR;

namespace Varus.Stopwatch.Web.Hubs
{
    public interface IStopwatchHubClient
    {
        void BroadcastElapsed(Dictionary<string, long> elapsedTimes);
    }

    public class StopwatchHub : Hub<IStopwatchHubClient>
    {
        // Used to keep track of user subscriptions of periodic stopwatch times.
        private static readonly ConcurrentDictionary<string, ConcurrentBag<IDisposable>> _elapsedSubscriptions
            = new ConcurrentDictionary<string, ConcurrentBag<IDisposable>>();

        private readonly IStopwatchService _service;
        private readonly IObservable<Unit> _elapsedInterval;

        public StopwatchHub(IStopwatchService service, IObservable<Unit> elapsedInterval)
        {
            _service = service;

            // A timer for periodically broadcasting elapsed stopwatch times is abstracted
            // in order to ease testing.
            _elapsedInterval = elapsedInterval;
        }

        [AuthorizeClaim("Basic", "API-Key")]
        public Task GetAsync(string user)
        {
            // Periodically start broadcasting elapsed times to the user for all his/her stopwatches.
            var subscription = _elapsedInterval
                .SelectMany(_ => _service.MapElapsedTimesByNameAsync(user))
                .Subscribe(result => Clients.Caller.BroadcastElapsed(result));

            // Store the timer subscription in order to dispose of it when the connection disconnects.
            var subscriptions = GetOrAddSubscriptions(Context.ConnectionId);
            subscriptions.Add(subscription);

            return Task.FromResult<object>(null);
        }

        [AuthorizeClaim("Basic")]
        public async Task PostAsync(string name)
        {
            string user = ((ClaimsPrincipal)Context.Request.User).Claims
                .First(claim => claim.Type == "User").Value;
            await _service.RestartStopwatchAsync(user, name);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Dispose of any timer subscriptions associated with the connection.
            if (_elapsedSubscriptions.TryRemove(Context.ConnectionId, out var subscriptions))
            {
                foreach (var subscription in subscriptions)
                    subscription.Dispose();
            }
            return Task.FromResult<object>(null);
        }

        private static ConcurrentBag<IDisposable> GetOrAddSubscriptions(string userId)
        {
            if (!_elapsedSubscriptions.TryGetValue(userId, out var subscriptions))
            {
                subscriptions = new ConcurrentBag<IDisposable>();
                subscriptions = _elapsedSubscriptions.GetOrAdd(userId, subscriptions);
            }
            return subscriptions;
        }
    }
}