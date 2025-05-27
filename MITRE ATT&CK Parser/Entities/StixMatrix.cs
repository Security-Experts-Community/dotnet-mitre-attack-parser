using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixMatrix : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "x-mitre-matrix";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("tactic_refs")]
        public List<string> TacticRefs { get; set; } = new List<string>();
    }
}
