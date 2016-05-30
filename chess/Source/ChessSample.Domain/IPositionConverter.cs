namespace ChessSample.Domain
{
    /// <summary>
    /// Provides an interface for converting chess square position values
    /// between numeric and textual representations.
    /// </summary>
    public interface IPositionConverter
    {
        /// <summary>
        /// Converts text to coordinates.
        /// </summary>
        /// <param name="position">Textual representation.</param>
        /// <returns>Position coordinates.</returns>
        Point ToCoordinates(string position);

        /// <summary>
        /// Converts coordinates to text.
        /// </summary>
        /// <param name="position">Coordinate x, y representation.</param>
        /// <returns>Position textual representation.</returns>
        string ToText(Point position);
    }
}
