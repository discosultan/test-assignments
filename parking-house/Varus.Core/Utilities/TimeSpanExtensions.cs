using System;

namespace Varus.Core.Utilities
{
    /// <summary>
    /// Defines extension methods for <see cref="TimeSpan"/> struct.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Multiplies a timespan by an integer value.
        /// </summary>
        public static TimeSpan Multiply(this TimeSpan multiplicand, int multiplier)
        {
            return TimeSpan.FromTicks(multiplicand.Ticks * multiplier);
        }
    }
}
