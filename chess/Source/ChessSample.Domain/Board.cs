using System;
using System.Collections.Generic;
using ChessSample.Domain.Pieces;

namespace ChessSample.Domain
{
    /// <summary>
    /// A chess board made of x*y squares.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Constructs a new instance of <see cref="Board"/>.
        /// </summary>
        /// <param name="horizontalLength">Number of squares horizontally.</param>
        /// <param name="verticalLength">Number of squares vertically.</param>
        /// <param name="impassablePositions">A collection of impassable square positions.</param>
        public Board(int horizontalLength, int verticalLength, params Point[] impassablePositions)
        {
            // Initialize board squares.
            Squares = new Square[horizontalLength, verticalLength];
            for (int x = 0; x < horizontalLength; ++x)
            {
                for (int y = 0; y < verticalLength; ++y)
                {
                    Squares[x, y] = new Square();
                }
            }

            // If no impassable positions have been specified, return ctor.
            if (impassablePositions == null) return;

            foreach (Point impassablePosition in impassablePositions)
            {
                // Make sure that the specified impassable position does not lie outside the board.
                if (!IsInBounds(impassablePosition))
                    throw new ArgumentException("Impassable positions must be within board bounds.", "impassablePositions");

                // Mark impassable square as blocked.
                Squares[impassablePosition.X, impassablePosition.Y].IsBlocked = true;
            }
        }

        /// <summary>
        /// Gets the board squares as a two-dimensional array.
        /// </summary>
        public Square[,] Squares { get; private set; }

        /// <summary>
        /// Gets the number of squares horizontally.
        /// </summary>
        public int Width
        {
            get { return Squares.GetLength(0); }
        }

        /// <summary>
        /// Gets the number of squares vertically.
        /// </summary>
        public int Height
        {
            get { return Squares.GetLength(1); }
        }

        /// <summary>
        /// Gets a collection of all the pieces on the board.
        /// </summary>
        /// <remarks>
        /// If there was a case we needed to access this property
        /// often, it would be wise to keep a separate collection of all
        /// the board pieces.
        /// </remarks>
        public IEnumerable<Piece> Pieces
        {
            get
            {
                var pieces = new List<Piece>();
                for (int x = 0; x < Width; ++x)
                    for (int y = 0; y < Height; ++y)
                    {
                        Square square = Squares[x, y];
                        if (square.HasPiece)
                            pieces.Add(square.Piece);
                    }
                return pieces;
            }
        }

        /// <summary>
        /// Checks if a position is within the board bounds.
        /// </summary>
        /// <param name="position">The x, y coordinate to check.</param>
        /// <returns>True if within board bounds; otherwise false.</returns>
        public bool IsInBounds(Point position)
        {
            return position.X >= 0 &&
                   position.Y >= 0 &&
                   position.X < Width &&
                   position.Y < Height;
        }

        /// <summary>
        /// Checks if a position is within the board bounds.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>True if within board bounds; otherwise false.</returns>
        public bool IsInBounds(int x, int y)
        {
            return IsInBounds(new Point(x, y));
        }

        /// <summary>
        /// Places a new <see cref="Piece"/> on the board.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Piece"/> to place (i.e Pawn, Knight etc).</typeparam>
        /// <param name="position">The x, y coordinates of the <see cref="Piece"/> on the board.</param>
        /// <returns>The newly placed <see cref="Piece"/>.</returns>
        public Piece PlaceNewPiece<T>(Point position) where T : Piece, new()
        {
            if (!IsInBounds(position))
                throw new ArgumentException
                    (string.Format("Invalid location. The piece must be placed within the bounds."),
                    "position");

            Square square = Squares[position.X, position.Y];

            if (square.IsBlocked)
                throw new ArgumentException(
                    string.Format("Square {0} is already occupied by another piece or is blocked.", position));

            Piece newPiece = new T();
            newPiece.Position = position;
            newPiece.Board = this;

            square.Piece = newPiece;

            return newPiece;
        }

        /// <summary>
        /// Places a new <see cref="Piece"/> on the board.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="Piece"/> to place (i.e Pawn, Knight etc).</typeparam>
        /// <param name="x">The x coordinate of the <see cref="Piece"/> on the board.</param>
        /// <param name="y">The y coordinate of the <see cref="Piece"/> on the board.</param>
        /// <returns>The newly placed <see cref="Piece"/>.</returns>
        public Piece PlaceNewPiece<T>(int x, int y) where T : Piece, new()
        {
            return PlaceNewPiece<T>(new Point(x, y));
        }
    }
}
