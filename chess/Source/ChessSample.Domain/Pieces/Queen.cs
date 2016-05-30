using System.Collections.Generic;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Chess board <see cref="Piece"/>. Can move any number of squares
    /// diagonally, horizontally or vertically.
    /// </summary>
    public class Queen : Piece
    {
        protected override IEnumerable<Point> GetPossibleMoves(Point currentPosition)
        {
            throw new System.NotImplementedException();
        }  
    }
}
