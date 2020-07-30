using System.Collections.Generic;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Chess <see cref="Board"/> piece. Can move 1 square horizontally and 2 vertically or
    /// 2 horizontally and 1 vertically.
    /// </summary>
    public class Knight : Piece
    {
        private static readonly Point[] PossibleMoves =
        {
            new Point(1, 2),
            new Point(1, -2),
            new Point(-1, 2),
            new Point(-1, -2),
            new Point(2, 1),
            new Point(2, -1),
            new Point(-2, 1),
            new Point(-2, -1)
        };

        protected override IEnumerable<Point> GetPossibleMoves(Point currentPosition)
        {
            return PossibleMoves;
        }
    }
}
