using System;
using System.Collections.Generic;
using ChessSample.Domain;

namespace ChessSample.CommandLine
{
    class InputData
    {
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public Type PieceType { get; set; }
        public Point StartingPosition { get; set; }
        public Point TargetPosition { get; set; }
        public Point[] BlockedSquares { get; set; }
    }

    class OutputData
    {
        public Point StartingPosition { get; set; }
        public int NumberOfMoves { get; set; }
        public IEnumerable<Path> Moves { get; set; }
    }
}
