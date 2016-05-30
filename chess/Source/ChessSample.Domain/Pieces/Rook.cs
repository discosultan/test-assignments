using System.Collections.Generic;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Chess board <see cref="Piece"/>. Can move any number of squares
    /// horizontally or vertically.
    /// </summary>
    public class Rook : Piece
    {
        protected override IEnumerable<Point> GetPossibleMoves(Point currentPosition)
        {
            var result = new List<Point>();

            // Left.
            for (int i = 1; i <= currentPosition.X; ++i)
            {
                result.Add(new Point(-i, 0));
            }
            // Right.
            for (int i = 1; i <= Board.Width - currentPosition.X - 1; ++i)
            {
                result.Add(new Point(i, 0));
            }
            // Top.
            for (int i = 1; i <= currentPosition.Y; ++i)
            {
                result.Add(new Point(0, -i));
            }
            // Bottom.
            for (int i = 1; i <= Board.Height - currentPosition.Y - 1; ++i)
            {
                result.Add(new Point(0, i));
            }

            return result;
        }  
    }
}
