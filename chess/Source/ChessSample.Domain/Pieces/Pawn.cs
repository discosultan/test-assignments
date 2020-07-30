using System.Collections.Generic;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Chess board <see cref="Piece"/>. Can move two squares upward when in
    /// starting position; otherwise one square upward.
    /// </summary>
    public class Pawn : Piece
    {
        protected override IEnumerable<Point> GetPossibleMoves(Point currentPosition)
        {
            throw new System.NotImplementedException();
        }
    }
}
