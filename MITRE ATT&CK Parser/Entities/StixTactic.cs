using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixTactic : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "x-mitre-tactic";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("x_mitre_shortname")]
        public string MitreShortname { get; set; }
    }
}
