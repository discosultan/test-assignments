using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChessSample.Domain
{
    /// <summary>
    /// Provides coordinate conversions between x, y coordinate and
    /// letter digit textual formats (i.e {x:1,y:2} -> "B3").
    /// </summary>
    public class LetterDigitPositionConverter : IPositionConverter
    {
        private static readonly Regex Regex = new Regex(@"^[a-zA-Z]+[1-9]\d*?$");
        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        /// <inherit/>
        public Point ToCoordinates(string position)
        {
            if (string.IsNullOrWhiteSpace(position))
                throw new ArgumentException("Position cannot be null or empty.", "position");

            if (!Regex.IsMatch(position))
                throw new ArgumentException(string.Format("Position must be of format {0}.", Regex));

            int indexOfFirstDigit = position.IndexOfAny("0123456789".ToCharArray());

            string xPart = position.Substring(0, indexOfFirstDigit);
            string yPart = position.Substring(indexOfFirstDigit);

            int x = xPart.Select((t, i) => i * ('Z' - 'A') + (char.ToUpper(t) - 'A')).Sum();
            int y = int.Parse(yPart) - 1;

            return new Point(x, y);
        }

        /// <inherit/>
        public string ToText(Point position)
        {
            string xPart = "";
            int x = position.X;
            const int charBase = 'Z' - 'A' + 1;
            do
            {
                var c = (char)(x % charBase + 'A');
                xPart += c;
                x /= charBase;
            } while (x > 0);

            string yPart = (position.Y + 1).ToString(Culture);

            return xPart + yPart;
        }
    }
}
