using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Varus.Core;

namespace Varus.Parking.UnitTests
{
    /// <summary>
    /// Provides infrastructure for a set of tests on a given aggregate.
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    public abstract class BehaviorDrivenTests<TAggregate>
        where TAggregate : Aggregate
    {
        private class CommandHandlerNotDefiendException : Exception
        {
            public CommandHandlerNotDefiendException(string msg) : base(msg) { }
        }

        private TAggregate _aggregate;

        [SetUp]
        public void BehaviorDrivenTestSetup()
        {
            _aggregate = BuildNewAggregate();
        }

        protected abstract TAggregate BuildNewAggregate();

        protected void Test(IEnumerable<Event> given, Func<TAggregate, object> when, Action<object> then)
        {
            then(when(ApplyEvents(_aggregate, given)));
        }

        protected IEnumerable<Event> Given(params Event[] events)
        {
            return events;
        }

        protected Func<TAggregate, object>When<TCommand>(TCommand command) where TCommand : Command
        {
            return agg =>
            {
                try
                {
                    return DispatchCommand(command).ToArray();
                }
                catch (Exception e)
                {
                    return e;
                }
            };
        }

        protected Action<object> Then(params Event[] expectedEvents)
        {
            return got =>
            {
                var gotEvents = got as Event[];
                if (gotEvents != null)
                {
                    if (gotEvents.Count() == expectedEvents.Length)
                        for (var i = 0; i < gotEvents.Length; i++)
                            if (gotEvents[i].GetType() == expectedEvents[i].GetType())
                                Assert.AreEqual(Serialize(expectedEvents[i]), Serialize(gotEvents[i]));
                            else
                                Assert.Fail("Incorrect event in results; expected a {0} but got a {1}", expectedEvents[i].GetType().Name, gotEvents[i].GetType().Name);
                    else if (gotEvents.Length < expectedEvents.Length)
                        Assert.Fail("Expected event(s) missing: {0}", string.Join(", ", GetEventDifferences(expectedEvents, gotEvents)));
                    else
                        Assert.Fail("Unexpected event(s) emitted: {0}", string.Join(", ", GetEventDifferences(gotEvents, expectedEvents)));
                }
                else if (got is CommandHandlerNotDefiendException)
                    Assert.Fail(((Exception) got).Message);
                else
                    Assert.Fail("Expected events, but got exception {0}",
                        got.GetType().Name);
            };
        }

        private static string[] GetEventDifferences(IEnumerable<object> seq1, IEnumerable<object> seq2)
        {
            var diff = seq1.Select(e => e.GetType().Name).ToList();
            foreach (var remove in seq2.Select(e => e.GetType().Name))
                diff.Remove(remove);
            return diff.ToArray();
        }

        protected Action<object> ThenFailWith<TException>()
        {
            return got =>
            {
                if (got is TException)
                    Assert.Pass("Got correct exception type");
                else if (got is CommandHandlerNotDefiendException)
                    Assert.Fail(((Exception) got).Message);
                else if (got is Exception)
                    Assert.Fail("Expected exception {0}, but got exception {1}", typeof(TException).Name, got.GetType().Name);
                else
                    Assert.Fail("Expected exception {0}, but got event result", typeof(TException).Name);
            };
        }

        private IEnumerable<Event> DispatchCommand<TCommand>(TCommand c) where TCommand : Command
        {
            var handler = _aggregate as IHandleCommand<TCommand>;
            if (handler == null)
                throw new CommandHandlerNotDefiendException(string.Format(
                    "Aggregate {0} does not yet handle command {1}",
                    _aggregate.GetType().Name, c.GetType().Name));
            return handler.Handle(c);
        }

        private static TAggregate ApplyEvents(TAggregate agg, IEnumerable<Event> events)
        {
            agg.ApplyEvents(events);
            return agg;
        }

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }        
    }
}
