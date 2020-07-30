using System.Collections.Generic;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Chess board <see cref="Piece"/>. Can move any number of squares
    /// diagonally.
    /// </summary>
    public class Bishop : Piece
    {
        protected override IEnumerable<Point> GetPossibleMoves(Point currentPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
