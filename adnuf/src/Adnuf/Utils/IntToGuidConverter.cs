using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adnuf.Utils
{
    /// <summary>
    /// Custom JSON converter to convert an incoming JSON integer to a <see cref="Guid"/>.
    /// See also <seealso cref="IntExtensions.ToGuid(int)"/>.
    /// </summary>
    public class IntToGuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => reader.GetInt32().ToGuid();

        public override void Write(
            Utf8JsonWriter writer,
            Guid value,
            JsonSerializerOptions options) => throw new NotImplementedException();
    }
}
