using System;

namespace Adnuf.Utils
{
    /// <summary>
    /// Provides extension methods for the <see cref="int"/> type.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Converts the bytes that make up an <see cref="int"/> to a <see cref="Guid"/>.
        /// </summary>
        public static Guid ToGuid(this int value)
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
