using System;

namespace ChessSample.Domain
{
    /// <summary>
    /// Represents a two-dimensional integer coordinate.
    /// </summary>
    public struct Point : IEquatable<Point>
    {
        /// <summary>
        /// A point with (0,0) coordinates.
        /// </summary>
        public static readonly Point Zero = new Point(0, 0);

        /// <summary>
        /// Left coordinate.
        /// </summary>
        public int X;

        /// <summary>
        /// Top coordinate.
        /// </summary>
        public int Y;

         /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <inheritdoc/>
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        /// <inheritdoc/>
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        /// <inheritdoc/>
        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        /// <inheritdoc/>
        public static Point operator /(Point a, Point b)
        {
            return new Point(a.X / b.X, a.Y / b.Y);
        }

        /// <inheritdoc/>
        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Point"/> is equal to this instance.
        /// Two <see cref="Point"/>s are equal if and only if both X and Y values are equal.
        /// </summary>
        /// <param name="other">The <see cref="Point"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Point"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Point other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return (obj is Point) && Equals((Point)obj);
        }

        /// <inheritdoc/>
        /// <remarks>http://stackoverflow.com/a/102878/1466456</remarks>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", X, Y);
        }
    }
}
