using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixDataComponent : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "x-mitre-data-component";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("x_mitre_data_source_ref")]
        public string MitreDataSourceRef { get; set; }
    }
}
