using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixIdentity : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "identity";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("identity_class")]
        public string IdentityClass { get; set; }

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; } = new List<string>();

        [JsonPropertyName("sectors")]
        public List<string> Sectors { get; set; } = new List<string>();
    }
}
