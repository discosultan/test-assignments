using System;
using System.Collections.Generic;

namespace Varus.Core
{
    public interface IEventStore
    {
        IEnumerable<Event> LoadEventsFor<TAggregate>(Guid id) where TAggregate : Aggregate;
        void SaveEventsFor<TAggregate>(Guid id, IEnumerable<Event> events) where TAggregate : Aggregate;
    }
}
