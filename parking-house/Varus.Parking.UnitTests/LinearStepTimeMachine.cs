using System;
using Varus.Parking.Domain;

namespace Varus.Parking.UnitTests
{
    /// <summary>
    /// Concept of time where time is initialized to a certain value and increased linearly
    /// by the amount specified each time it is accessed. Perfect for simulating domain processes.
    /// </summary>
    public class LinearStepTimeMachine : ITimeMachine
    {
        private DateTime _currentTime;
        private readonly TimeSpan _timeStep;

        /// <summary>
        /// Constructs a new instance of <see cref="LinearStepTimeMachine"/>.
        /// </summary>
        /// <param name="initialTime">The initial time the time machine starts advancing from.</param>
        /// <param name="timeStep">The amount of time to advance each time the time machine is accessed.</param>
        public LinearStepTimeMachine(DateTime initialTime, TimeSpan timeStep)
        {
            _currentTime = initialTime;
            _timeStep = timeStep;
        }

        /// <summary>
        /// Queries for current datetime and advances time by the amount
        /// specified by time step.
        /// </summary>
        /// <returns></returns>
        public DateTime Now()
        {
            DateTime returnValue = _currentTime;
            _currentTime += _timeStep;
            return returnValue;
        }
    }
}
