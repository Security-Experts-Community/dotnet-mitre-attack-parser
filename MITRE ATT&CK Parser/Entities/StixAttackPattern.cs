using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixAttackPattern : StixObject
    {

        [JsonPropertyName("type")]
        public override string Type => "attack-pattern";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("kill_chain_phases")]
        public List<MitreKillChainPhase> KillChainPhases { get; set; } = new List<MitreKillChainPhase>();

        [JsonPropertyName("x_mitre_detection")]
        public string MitreDetection { get; set; }

        [JsonPropertyName("x_mitre_platforms")]
        public List<string> MitrePlatforms { get; set; } = new List<string>();

        [JsonPropertyName("x_mitre_is_subtechnique")]
        public bool MitreIsSubtechnique { get; set; }

        [JsonPropertyName("x_mitre_data_sources")]
        public List<string> MitreDataSources { get; set; } = new List<string>();
        public class MitreKillChainPhase
        {
            [JsonPropertyName("kill_chain_name")]
            public string KillChainName { get; set; }

            [JsonPropertyName("phase_name")]
            public string PhaseName { get; set; }
        }
    }
}
