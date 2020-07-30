using ChessSample.Domain.Pieces;

namespace ChessSample.Domain
{
    /// <summary>
    /// Represents a square on a <see cref="Board"/>.
    /// </summary>
    public class Square
    {
        /// <summary>
        /// A <see cref="Piece"/> on the square. This will be null if the
        /// square doesn't contain a <see cref="Piece"/>.
        /// </summary>
        public Piece Piece { get; internal set; }

        /// <summary>
        /// Gets if the square is blocked, meaning no <see cref="Piece"/> can be
        /// placed on it or move through it.
        /// </summary>
        public bool IsBlocked { get; internal set; }

        /// <summary>
        /// Gets if the square contains a <see cref="Piece"/>.
        /// </summary>
        public bool HasPiece
        {
            get { return !IsBlocked && Piece != null; }
        }
    }
}
