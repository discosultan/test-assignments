using System.Collections.Generic;

namespace Pronoodle.Products
{
    /// <summary>
    /// A structure for a batch operation to track both individual successes and errors.
    /// </summary>
    /// <typeparam name="T">Type of success result.</typeparam>
    /// <typeparam name="E">Type of error result.</typeparam>
    public class BatchResult<T, E>
    {
        /// <summary>
        /// Gets a list of successful batch operation results.
        /// </summary>
        public List<T> Successes { get; } = new List<T>();

        /// <summary>
        /// Gets a list of errored batch operation results.
        /// </summary>
        public List<E> Errors { get; } = new List<E>();
    }
}
