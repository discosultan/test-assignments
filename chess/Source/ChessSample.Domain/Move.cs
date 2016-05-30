using ChessSample.Domain.Pieces;

namespace ChessSample.Domain
{
    /// <summary>
    /// Represents a <see cref="Piece"/> movement from one <see cref="Square"/> to another.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Constructs a new instance of <see cref="Move"/>.
        /// </summary>
        /// <param name="from">The position the move started.</param>
        /// <param name="to">The position the move ended.</param>
        public Move(Point from, Point to)
        {
            From = from;
            To = to;
        }

        /// <summary>
        /// Gets the position where the move started.
        /// </summary>
        public Point From { get; private set; }

        /// <summary>
        /// Gets the position where to move ended.
        /// </summary>
        public Point To { get; private set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("{0} -> {1}", From, To);
        }
    }
}
