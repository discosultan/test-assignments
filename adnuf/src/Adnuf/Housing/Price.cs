using System.Text.Json.Serialization;

namespace Adnuf.Housing
{
    public class Price
    {
        [JsonPropertyName("Koopprijs")]
        public decimal? Value { get; }

        [JsonPropertyName("KoopAbbreviation")]
        public string Suffix { get; }

        public string Symbol { get; } = "€";
    }
}
