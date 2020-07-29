using Adnuf.Utils;
using System;
using System.Text.Json.Serialization;

namespace Adnuf.Housing
{
    public class Property : Entity
    {
        // We tie ourselves to Funda's model with those annotations. In order to pollute core
        // entities less, we could setup mappings in a separate file (similar to Entity Framework's
        // fluent API mappings).
        [JsonPropertyName("Adres")]
        public string Address { get; set; }

        [JsonPropertyName("Woonplaats")]
        public string City { get; set; }

        public string Postcode { get; set; }

        [JsonPropertyName("Prijs")]
        public Price Price { get; set; }

        [JsonPropertyName("AantalKamers")]
        public int? Rooms { get; set; }

        [JsonPropertyName("Woonoppervlakte")]
        public int? LivingArea { get; set; }

        [JsonPropertyName("Perceeloppervlakte")]
        public int? PlotArea { get; set; }

        [JsonPropertyName("MakelaarId")]
        [JsonConverter(typeof(IntToGuidConverter))]
        public Guid AgentId { get; set; }

        [JsonPropertyName("MakelaarNaam")]
        public string AgentName { get; set; }
    }
}
