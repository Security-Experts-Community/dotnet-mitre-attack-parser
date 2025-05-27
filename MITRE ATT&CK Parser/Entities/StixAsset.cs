using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixAsset : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "x-mitre-asset";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("x_mitre_sectors")]
        public List<string> Sectors { get; set; } = new();

        [JsonPropertyName("x_mitre_platforms")]
        public List<string> Platforms { get; set; } = new();

        [JsonPropertyName("x_mitre_related_assets")]
        public List<RelatedAsset> RelatedAssets { get; set; } = new();

        public class RelatedAsset
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("related_asset_sectors")]
            public List<string> Sectors { get; set; } = new();
        }
    }
}
