namespace ChessSample.Domain
{
    /// <summary>
    /// Implementing class supports deep copying.
    /// </summary>
    /// <typeparam name="T">Type to deep copy.</typeparam>
    interface IDeepCopiable<out T> where T : class
    {
        /// <summary>
        /// Creates a deep copy of <see cref="T"/>.
        /// </summary>
        /// <returns>Deep copy of <see cref="T"/>.</returns>
        T DeepCopy();
    }
}
