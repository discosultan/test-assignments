using System;
using System.Linq;
using ChessSample.Domain;
using ChessSample.Domain.Pieces;

namespace ChessSample.CommandLine
{
    class Program
    {        
        static void Main(string[] args)
        {
            const StringComparison comparisonMethod = StringComparison.OrdinalIgnoreCase;
            IFileHandler fileHandler = new FileHandler(new LetterDigitPositionConverter());
            
            // Read command line args.
            string inputPath = null;
            string outputPath = null;
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].Equals("/I", comparisonMethod))
                {
                    inputPath = args[++i];                    
                    continue;
                }
                if (args[i].Equals("/O", comparisonMethod))
                {
                    outputPath = args[++i];
                }
            }

            // Read input file.
            InputData input = fileHandler.ParseInputFile(inputPath);

            // Do stuff :)
            var board = new Board(input.BoardWidth, input.BoardHeight, input.BlockedSquares);                        
            var piece = (Piece)typeof(Board)
                .GetMethod("PlaceNewPiece", new [] { typeof(Point) })
                .MakeGenericMethod(new [] { input.PieceType })
                .Invoke(board, new object[] { input.StartingPosition });
            Path[] paths = piece.FindShortestPathsTo(input.TargetPosition).ToArray();            
            var output = new OutputData
            {
                NumberOfMoves = paths[0].NumberOfMoves,
                Moves = paths,
                StartingPosition = input.StartingPosition
            };

            // Write output file.
            fileHandler.WriteOutputFile(outputPath, output);
        }
    }
}
