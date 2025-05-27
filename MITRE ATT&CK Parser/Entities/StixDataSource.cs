using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixDataSource : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "x-mitre-data-source";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("x_mitre_platforms")]
        public List<string> MitrePlatforms { get; set; } = new List<string>();

        [JsonPropertyName("x_mitre_collection_layers")]
        public List<string> MitreCollectionLayers { get; set; } = new List<string>();
    }
}
