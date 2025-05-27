using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixCampaign : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "campaign";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> Aliases { get; set; } = new List<string>();

        [JsonPropertyName("first_seen")]
        public DateTime FirstSeen { get; set; }

        [JsonPropertyName("last_seen")]
        public DateTime LastSeen { get; set; }

        [JsonPropertyName("x_mitre_first_seen_citation")]
        public string MitreFirstSeenCitation { get; set; }

        [JsonPropertyName("x_mitre_last_seen_citation")]
        public string MitreLastSeenCitation { get; set; }
    }
}
