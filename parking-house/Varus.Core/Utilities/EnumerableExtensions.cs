using System;
using System.Collections.Generic;

namespace Varus.Core.Utilities
{
    /// <summary>
    /// Defines extensions methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Immediately executes the given action on each element in the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence</typeparam>
        /// <param name="source">The sequence of elements</param>
        /// <param name="action">The action to execute on each element</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Check.ArgumentNotNull(source, "source");
            Check.ArgumentNotNull(action, "action");
            foreach (var element in source)
            {
                action(element);
            }
        }
    }
}
