using System;

namespace Varus.Parking.Domain
{    
    /// <summary>
    /// An immutable structure composed of width and length.
    /// </summary>
    public struct Size : IEquatable<Size>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Size"/> struct.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="length">The length.</param>
        public Size(int width, int length)
        {
            Width = width;
            Length = length;
        }
        
        /// <summary>
        /// The width component of the size.
        /// </summary>
        public readonly int Width;
        
        /// <summary>
        /// The length component of the size.
        /// </summary>
        public readonly int Length;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Size other)
        {
            return other.Width == Width && other.Width == Length;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(Size)) return false;            
            return Equals((Size)obj);            
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Width * 397) ^ Length;
            }
        }

        /// <inheritdoc/>
        public static Size operator +(Size left, Size right)
        {
            return new Size(left.Width + right.Width, left.Length + right.Length);
        }

        /// <inheritdoc/>
        public static Size operator -(Size left, Size right)
        {
            return new Size(left.Width - right.Width, left.Length - right.Length);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Size left, Size right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Size left, Size right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("({0},{1})", Width, Length);
        }
    }
}
