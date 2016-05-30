using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Varus.Core.Utilities;

namespace Varus.Core
{
    /// <summary>
    /// This implements a basic message dispatcher, driving the overall command handling
    /// and event application/distribution process. It is suitable for a simple, single
    /// node application that can safely build its subscriber list at startup and keep
    /// it in memory. Depends on some kind of event storage mechanism.
    /// </summary>
    public class MessageDispatcher
    {
        private readonly Dictionary<Type, Action<Command>> _commandHandlers =
            new Dictionary<Type, Action<Command>>();
        private readonly Dictionary<Type, List<Action<Event>>> _eventSubscribers =
            new Dictionary<Type, List<Action<Event>>>();
        private readonly IEventStore _eventStore;
        private readonly IDependencyResolver _dependencyResolver;

        /// <summary>
        /// Initializes a message dispatcher, which will use the specified event store
        /// implementation.
        /// </summary>
        /// <param name="eventStore">Event store to save messages to.</param>
        /// <param name="dependencyResolver"></param>
        public MessageDispatcher(IEventStore eventStore, IDependencyResolver dependencyResolver)
        {
            _eventStore = eventStore;
            _dependencyResolver = dependencyResolver;
        }

        /// <summary>
        /// Tries to send the specified command to its handler. Throws an exception
        /// if there is no handler registered for the command.
        /// </summary>
        /// <typeparam name="TCommand">Type of command.</typeparam>
        /// <param name="c">Command to send.</param>
        public void SendCommand<TCommand>(TCommand c) where TCommand : Command
        {
            Action<Command> action;
            if (!_commandHandlers.TryGetValue(typeof (TCommand), out action))
                throw new InvalidOperationException("No command handler registered for " + typeof (TCommand).Name);

            action(c);
        }

        /// <summary>
        /// Republishes all the events prior to specified datetime to subscribers.
        /// </summary>
        /// <typeparam name="TAggregate">Aggregate to load events for.</typeparam>
        /// <param name="id">Id of the aggregate.</param>
        /// <param name="until">All events prior to that deadline will be published.</param>
        public void RepublishEventsUntil<TAggregate>(Guid id, DateTime until) where TAggregate : Aggregate
        {
            _eventStore.LoadEventsFor<TAggregate>(id)
                .Where(evt => evt.DateTime <= until)
                .ForEach(PublishEvent);            
        }

        /// <summary>
        /// Publishes the specified event to all of its subscribers.
        /// </summary>
        /// <param name="e"></param>
        private void PublishEvent(Event e)
        {
            List<Action<Event>> subscribers;
            if (_eventSubscribers.TryGetValue(e.GetType(), out subscribers))            
                subscribers.ForEach(action => action(e));            
        }

        /// <summary>
        /// Registers an aggregate as being the handler for a particular
        /// command.
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TCommand"></typeparam>        
        public void AddHandlerFor<TCommand, TAggregate>()
            where TAggregate : Aggregate where TCommand : Command
        {
            Check.False(_commandHandlers.ContainsKey(typeof (TCommand)),
                "Command handler already registered for " + typeof (TCommand).Name);            

            _commandHandlers.Add(typeof(TCommand), command =>
            {
                // Create an empty aggregate.
                var aggregate = (TAggregate)CreateInstanceOf(typeof(TAggregate));
                aggregate.Id = command.Id;

                // Load the aggregate with events.
                aggregate.ApplyEvents(_eventStore.LoadEventsFor<TAggregate>(aggregate.Id));

                // With everything set up, we invoke the command handler, collecting the
                // events that it produces.
                Event[] resultEvents = ((IHandleCommand<TCommand>)aggregate).Handle((TCommand)command).ToArray();

                // Store the events in the event store.
                if (resultEvents.Length > 0)
                    _eventStore.SaveEventsFor<TAggregate>(aggregate.Id, resultEvents);

                // Publish them to all subscribers.
                resultEvents.ForEach(PublishEvent);                
            });
        }

        /// <summary>
        /// Adds an object that subscribes to the specified event, by virtue of implementing
        /// the ISubscribeTo interface.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="subscriber"></param>
        public void AddSubscriberFor<TEvent>(ISubscribeTo<TEvent> subscriber) where TEvent : Event
        {            
            List<Action<Event>> subscribers;
            if (!_eventSubscribers.TryGetValue(typeof(TEvent), out subscribers))
            {
                subscribers = new List<Action<Event>>();
                _eventSubscribers.Add(typeof(TEvent), subscribers);
            }            
            subscribers.Add(evt => subscriber.Handle((TEvent)evt));
        }

        /// <summary>
        /// Looks thorugh the specified assembly for all public types that implement
        /// the IExecuteCommand or IHandleEvent generic interfaces. Registers each of
        /// the implementations as a command handler or event subscriber.
        /// </summary>
        /// <param name="ass"></param>
        public void ScanAssembly(Assembly ass)
        {
            ass.GetTypes().ForEach(type => ScanInstance(CreateInstanceOf(type)));
        }        

        /// <summary>
        /// Looks at the specified object type, examples what commands it handles
        /// or events it subscribes to, and registers it as a receiver/subscriber.
        /// </summary>
        /// <typeparam name="T">Type of object to scan for.</typeparam >
        public void ScanType<T>()
        {
            ScanInstance(CreateInstanceOf(typeof(T)));
        }

        /// <summary>
        /// Looks at the specified object instance, examples what commands it handles
        /// or events it subscribes to, and registers it as a receiver/subscriber.
        /// </summary>
        /// <param name="instance">Object instance to scan for.</param >
        public void ScanInstance(object instance)
        {
            // Scan for and register handlers.
            Type type = instance.GetType();
            var handlers = type
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == typeof (IHandleCommand<>))
                .Select(i =>
                {
                    Type[] args = i.GetGenericArguments();
                    return new
                    {
                        CommandType = args[0],
                        AggregateType = type
                    };
                });
            handlers.ForEach(handler => GetType()
                .GetMethod("AddHandlerFor")
                .MakeGenericMethod(handler.CommandType, handler.AggregateType)
                .Invoke(this, new object[] { }));

            // Scan for and register subscribers.
            var subscribers = type
                .GetInterfaces()
                .Where(i => i.IsGenericType)
                .Where(i => i.GetGenericTypeDefinition() == typeof (ISubscribeTo<>))
                .Select(i => i.GetGenericArguments()[0]);
            subscribers.ForEach(subscriber => GetType()
                .GetMethod("AddSubscriberFor")
                .MakeGenericMethod(subscriber)
                .Invoke(this, new[] { instance }));
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">Type of <see cref="Aggregate"/> to create.</param>
        /// <returns>Newly created <see cref="Aggregate"/>.</returns>
        private object CreateInstanceOf(Type type)
        {
            try
            {
                return _dependencyResolver.GetObject(type);
            }
            catch (Exception)
            {
                return Activator.CreateInstance(type);
            }            
        }
    }
}
