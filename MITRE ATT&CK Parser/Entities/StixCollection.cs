using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixCollection : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "x-mitre-collection";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("x_mitre_contents")]
        public List<MitreContent> Contents { get; set; } = new List<MitreContent>();

        [JsonPropertyName("x_mitre_domains")]
        public List<string> Domains { get; set; } = new List<string>();
        public class MitreContent
        {
            [JsonPropertyName("object_ref")]
            public string ObjectRef { get; set; }

            [JsonPropertyName("object_modified")]
            public DateTime ObjectModified { get; set; }
        }

    }
}
