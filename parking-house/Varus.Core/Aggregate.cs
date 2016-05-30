using System;
using System.Collections.Generic;

namespace Varus.Core
{
    /// <summary>
    /// Aggregate base class, which factors out some common infrastructure that
    /// all aggregates have.
    /// </summary>
    public abstract class Aggregate
    {
        /// <summary>
        /// The unique ID of the aggregate.
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// The number of events loaded into this aggregate.
        /// </summary>
        public int NumEventsApplied { get; private set; }

        /// <summary>
        /// Enumerates the supplied events and applies them in order to the aggregate.
        /// </summary>
        /// <param name="events">Events to apply.</param>
        public void ApplyEvents(IEnumerable<Event> events)
        {
            foreach (var e in events)                
                GetType().GetMethod("ApplyEvent")
                    .MakeGenericMethod(e.GetType())
                    .Invoke(this, new object[] { e });
        }

        /// <summary>
        /// Applies a single event to the aggregate.
        /// </summary>
        /// <typeparam name="TEvent">Type of applied event.</typeparam>
        /// <param name="ev">Event to apply.</param>
        public void ApplyEvent<TEvent>(TEvent ev) where TEvent : Event
        {
            var applier = this as IApplyEvent<TEvent>;
            if (applier == null)
                throw new InvalidOperationException(string.Format(
                    "Aggregate {0} does not know how to apply event {1}",
                    GetType().Name, ev.GetType().Name));
            applier.Apply(ev);
            NumEventsApplied++;
        }
    }
}
