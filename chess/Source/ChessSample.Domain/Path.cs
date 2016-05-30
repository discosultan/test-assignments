using System.Collections.Generic;
using ChessSample.Domain.Pieces;

namespace ChessSample.Domain
{
    /// <summary>
    /// Represents a path for a specific <see cref="Piece"/> from source square
    /// to target square.
    /// </summary>
    public class Path : IDeepCopiable<Path>
    {
        private readonly List<Move> _moves = new List<Move>();

        internal void AddMove(Move move)
        {
            _moves.Add(move);
        }

        internal void AddMove(Point from, Point to)
        {
            AddMove(new Move(from, to));
        }

        /// <summary>
        /// Gets all the moves in this path.
        /// </summary>
        public IEnumerable<Move> Moves { get { return _moves; } }

        /// <summary>
        /// Gets the number of moves in this path.
        /// </summary>
        public int NumberOfMoves { get { return _moves.Count; } }
        
        internal bool IsFinished { get; set; }

        /// <summary>
        /// Creates a deep copy of this path.
        /// </summary>
        /// <returns>A deep copy of this path.</returns>
        public Path DeepCopy()
        {
            var copy = new Path();
            copy._moves.AddRange(_moves);
            return copy;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Join(", ", _moves);
        }
    }
}
