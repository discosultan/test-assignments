using System;
using System.Collections.Generic;
using System.Linq;

namespace Varus.Core
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<Guid, List<Event>> _store = new Dictionary<Guid, List<Event>>();
        private readonly object _lock = new object();

        public IEnumerable<Event> LoadEventsFor<TAggreage>(Guid id) where TAggreage : Aggregate
        {
            lock (_lock)
            {
                List<Event> events;
                return _store.TryGetValue(id, out events)
                    ? events
                    : Enumerable.Empty<Event>();
            }
        }

        public void SaveEventsFor<TAggregate>(Guid id, IEnumerable<Event> events) where TAggregate : Aggregate
        {
            lock (_lock)
            {
                List<Event> existingEvents;
                if (_store.TryGetValue(id, out existingEvents))
                    existingEvents.AddRange(events);
                else
                    _store.Add(id, events.ToList());
            }
        }
    }
}
