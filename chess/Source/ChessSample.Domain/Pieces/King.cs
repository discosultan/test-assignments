using System.Collections.Generic;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Chess board <see cref="Piece"/>. Can move 1 square to any
    /// direction.
    /// </summary>
    public class King : Piece
    {
        private static readonly Point[] PossibleMoves =
        {
            new Point(1, 0),
            new Point(1, 1),
            new Point(1, -1),
            new Point(-1, 0),
            new Point(-1, 1),
            new Point(-1, -1),
            new Point(0, 1),
            new Point(0, -1)
        };

        protected override IEnumerable<Point> GetPossibleMoves(Point currentPosition)
        {
            return PossibleMoves;
        }  
    }
}
