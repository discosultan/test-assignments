using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessSample.Domain.Pieces
{
    /// <summary>
    /// Movable chess board piece.
    /// </summary>
    public abstract class Piece
    {
        // Mark ctor as internal to force piece generation from board.
        internal Piece()
        {
        }

        /// <summary>
        /// Gets the current piece position on board.
        /// </summary>
        public Point Position { get; internal set; }

        /// <summary>
        /// Gets the board the piece is placed on.
        /// </summary>
        public Board Board { get; internal set; }

        /// <summary>
        /// Finds the shortests <see cref="Path"/>s to the target position.
        /// </summary>
        /// <param name="targetPosition">The x, y coordinatess of target position.</param>
        /// <returns>Collection of shortest <see cref="Path"/>s.</returns>
        public IEnumerable<Path> FindShortestPathsTo(Point targetPosition)
        {
            if (Board.Squares[targetPosition.X, targetPosition.Y].IsBlocked)
                throw new ArgumentException("Cannot find a path to a blocked position.", "targetPosition");

            // The list of all possible paths to target position.
            var paths = new List<Path>();

            // If we are already on the target position, return empty path collection.
            if (Position == targetPosition) return paths;

            // Search graph is used to keep track of visited squares and also the min number
            // of moves made to reach that square.
            var searchGraph = new Dictionary<Point, int>();

            // Starting path. This will branch later on.
            var result = new Path();

            // Begin attempting moves to all possible positions defined by piece type.
            AttemptPossibleMoves(searchGraph, Position, targetPosition, result, paths);

            // Find the shortest path out of all the possible paths.
            int minNrOfMoves = paths.Min(x => x.NumberOfMoves);

            // Return the shortest path or all shortest paths if many.
            return paths.Where(x => x.NumberOfMoves == minNrOfMoves);
        }

        /// <summary>
        /// Finds the shortests <see cref="Path"/>s to the target position.
        /// </summary>
        /// <param name="x">The x coordinate of target position.</param>
        /// <param name="y">The y coordinate of target position.</param>
        /// <returns>Collection of shortest <see cref="Path"/>s.</returns>
        public IEnumerable<Path> FindShortestPathsTo(int x, int y)
        {
            return FindShortestPathsTo(new Point(x, y));
        }

        protected abstract IEnumerable<Point> GetPossibleMoves(Point currentPosition);

        private void AttemptPossibleMoves(Dictionary<Point, int> searchGraph,
            Point currentPosition, Point targetPosition, Path result, List<Path> paths)
        {
            // Iterate over all the possible positions the piece can move from current position.
            foreach (Point possibleMove in GetPossibleMoves(currentPosition))
            {
                // Next possible position.
                Point nextPosition = currentPosition + possibleMove;

                // If the next position is not within board bounds, discard it and continue searching.
                if (!Board.IsInBounds(nextPosition)) continue;

                // Branch the current path to keep copies of all possible paths.
                Path branch = result.DeepCopy();

                AttemptMove(searchGraph, currentPosition, nextPosition, targetPosition, branch, paths);

                // If the branch is not finished (meaning that the target position is not yet reached),
                // continue branching.
                if (!branch.IsFinished) continue;

                // Add current branch as a potential path to target position and break
                // from further branching, because we know that there cannot be a shorter branch
                // or a branch with same lenght as current.
                paths.Add(branch);
                break;
            }
        }

        private void AttemptMove(Dictionary<Point, int> searchGraph,
            Point currentPosition, Point nextPosition, Point targetPosition, Path result,
            List<Path> possiblePaths)
        {
            // Number of moves made after this move.
            int numberOfMoves = result.NumberOfMoves + 1;

            // If the next position is target position, mark this path as finished.
            if (nextPosition == targetPosition)
            {
                result.IsFinished = true;
            }
            else
            {
                // Check if the next position is blocked.
                if (Board.Squares[nextPosition.X, nextPosition.Y].IsBlocked) return;

                // Check if there has already been a move to the next position.
                int previousMinNrOfMovesAtNextPos;
                if (searchGraph.TryGetValue(nextPosition, out previousMinNrOfMovesAtNextPos))
                {
                    // If the previous move on the target contained less total number of moves made, then
                    // it is pointless to search any further.
                    if (numberOfMoves > previousMinNrOfMovesAtNextPos) return;
                }
            }

            // Add the move from current to next position to the path.
            result.AddMove(currentPosition, nextPosition);

            if (result.IsFinished) return;

            // Set the total number of moves made at the next position.
            searchGraph[nextPosition] = numberOfMoves;

            // Continue searching. Attempt all the possible moves defined by piece type.
            AttemptPossibleMoves(searchGraph, nextPosition, targetPosition, result, possiblePaths);
        }
    }
}
