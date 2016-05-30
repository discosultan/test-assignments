using System;

namespace Varus.Parking.Domain
{
    /// <summary>
    /// Abstracts away the concept of time.
    /// </summary>
    public interface ITimeMachine
    {
        /// <summary>
        /// Queries for current datetime.
        /// </summary>
        /// <returns></returns>
        DateTime Now();
    }
}
