using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixTool : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "tool";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("x_mitre_aliases")]
        public List<string> MitreAliases { get; set; } = new List<string>();
    }
}
