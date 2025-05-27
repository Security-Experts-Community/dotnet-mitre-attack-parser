using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    [JsonPolymorphic]
    [JsonDerivedType(typeof(StixCollection), typeDiscriminator: "x-mitre-collection")]
    [JsonDerivedType(typeof(StixAttackPattern), typeDiscriminator: "attack-pattern")]
    [JsonDerivedType(typeof(StixCampaign), typeDiscriminator: "campaign")]
    [JsonDerivedType(typeof(StixCourseOfAction), typeDiscriminator: "course-of-action")]
    [JsonDerivedType(typeof(StixIdentity), typeDiscriminator: "identity")]
    [JsonDerivedType(typeof(StixIntrusionSet), typeDiscriminator: "intrusion-set")]
    [JsonDerivedType(typeof(StixMalware), typeDiscriminator: "malware")]
    [JsonDerivedType(typeof(StixRelationship), typeDiscriminator: "relationship")]
    [JsonDerivedType(typeof(StixTool), typeDiscriminator: "tool")]
    [JsonDerivedType(typeof(StixDataComponent), typeDiscriminator: "x-mitre-data-component")]
    [JsonDerivedType(typeof(StixDataSource), typeDiscriminator: "x-mitre-data-source")]
    [JsonDerivedType(typeof(StixMatrix), typeDiscriminator: "x-mitre-matrix")]
    [JsonDerivedType(typeof(StixTactic), typeDiscriminator: "x-mitre-tactic")]
    public abstract class StixObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore] 
        public abstract string Type { get; }

        [JsonPropertyName("spec_version")]
        public string SpecVersion { get; set; } = "2.1";

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("modified")]
        public DateTime Modified { get; set; }

        [JsonPropertyName("created_by_ref")]
        public string CreatedByRef { get; set; }

        [JsonPropertyName("object_marking_refs")]
        public List<string> ObjectMarkingRefs { get; set; } = new List<string>();

        [JsonPropertyName("external_references")]
        public List<MitreExternalReference> ExternalReferences { get; set; } = new List<MitreExternalReference>();

        [JsonPropertyName("x_mitre_modified_by_ref")]
        public string MitreModifiedByRef { get; set; }

        [JsonPropertyName("x_mitre_attack_spec_version")]
        public string MitreAttackSpecVersion { get; set; } = "3.2.0";

        [JsonPropertyName("x_mitre_deprecated")]
        public bool MitreDeprecated { get; set; }

        [JsonPropertyName("x_mitre_version")]
        public string MitreVersion { get; set; }
        public class MitreExternalReference
        {
            [JsonPropertyName("source_name")]
            public string SourceName { get; set; }

            [JsonPropertyName("external_id")]
            public string ExternalId { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }
    }
}
