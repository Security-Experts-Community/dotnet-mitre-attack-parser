using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixData
    {
        public StixCollection Collection { get; set; }
        public List<StixAttackPattern> AttackPatterns { get; set; }
        public List<StixCampaign> Campaigns { get; set; }
        public List<StixCourseOfAction> CourseOfActions { get; set; }
        public List<StixIdentity> Identities { get; set; }
        public List<StixIntrusionSet> IntrusionSets { get; set; }
        public List<StixMalware> Malwares { get; set; }
        public List<StixRelationship> Relationships { get; set; }
        public List<StixTool> Tools { get; set; }
        public List<StixDataComponent> DataComponents { get; set; }
        public List<StixDataSource> DataSources { get; set; }
        public List<StixMatrix> Matrices { get; set; }
        public List<StixTactic> Tactics { get; set; }
        public List<StixAsset>? Assets { get; set; }

        [JsonIgnore]
        public List<StixAsset> AssetsList => Assets ?? new List<StixAsset>();
    }
}
