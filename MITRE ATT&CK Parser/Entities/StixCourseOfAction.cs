using System.Text.Json.Serialization;

namespace MitreAttackParser.Entities
{
    public class StixCourseOfAction : StixObject
    {
        [JsonPropertyName("type")]
        public override string Type => "course-of-action";

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
