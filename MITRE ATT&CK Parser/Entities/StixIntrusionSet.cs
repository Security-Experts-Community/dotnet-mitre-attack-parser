using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixIntrusionSet : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "intrusion-set";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> Aliases { get; set; } = new List<string>();

        [JsonPropertyName("x_mitre_contributors")]
        public List<string> MitreContributors { get; set; } = new List<string>();
    }
}
