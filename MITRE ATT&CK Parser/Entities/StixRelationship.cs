using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixRelationship : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "relationship";

        [JsonPropertyName("relationship_type")]
        public string RelationshipType { get; set; }

        [JsonPropertyName("source_ref")]
        public string SourceRef { get; set; }

        [JsonPropertyName("target_ref")]
        public string TargetRef { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
