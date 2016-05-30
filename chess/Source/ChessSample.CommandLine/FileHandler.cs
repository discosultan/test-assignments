using System.IO;
using System.Linq;
using ChessSample.Domain;
using ChessSample.Domain.Pieces;
using Path = ChessSample.Domain.Path;

namespace ChessSample.CommandLine
{
    class FileHandler : IFileHandler
    {
        private readonly IPositionConverter _positionConverter;

        public FileHandler(IPositionConverter positionConverter)
        {
            _positionConverter = positionConverter;
        }

        public InputData ParseInputFile(string path)
        {
            var inputData = new InputData();
            using (StreamReader reader = File.OpenText(path))
            {
                inputData.BoardWidth = int.Parse(reader.ReadLine());
                inputData.BoardHeight = int.Parse(reader.ReadLine());
                string pieceType = UppercaseFirst(reader.ReadLine().Trim().ToLower());                    
                inputData.PieceType = typeof(Piece).Assembly.GetType(typeof(Piece).Namespace + "." + pieceType, true);
                inputData.StartingPosition = _positionConverter.ToCoordinates(ReadLine(reader));
                inputData.TargetPosition = _positionConverter.ToCoordinates(ReadLine(reader));                
                string[] blockedSquares = ReadLine(reader).Split(' ');
                inputData.BlockedSquares = new Point[blockedSquares.Length];
                for (int i = 0; i < blockedSquares.Length; ++i)
                {
                    string blockedSquare = blockedSquares[i].Trim();
                    if (blockedSquare.Contains(","))
                        blockedSquare = blockedSquare.Substring(0, blockedSquare.Length - 1);
                    inputData.BlockedSquares[i] = _positionConverter.ToCoordinates(blockedSquare);
                    
                }
            }
            return inputData;
        }

        public void WriteOutputFile(string path, OutputData data)
        {            
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(data.NumberOfMoves);
                const string separator = ", ";
                foreach (Path shortestPath in data.Moves)
                {
                    writer.Write(_positionConverter.ToText(data.StartingPosition) + separator);
                    writer.WriteLine(string.Join(separator, shortestPath.Moves.Select(x => _positionConverter.ToText(x.To))));
                }
                
            }
        }

        private static string ReadLine(StreamReader reader)
        {
            return reader.ReadLine().Trim().ToUpper();
        }

        private static string UppercaseFirst(string value)
        {
            char[] a = value.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
