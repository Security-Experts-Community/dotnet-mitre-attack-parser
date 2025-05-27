using MitreAttackParser.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MitreAttackParser.Helpers
{
    public class StixDataConverter : JsonConverter<StixData>
    {
         public override StixData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
         {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            var data = new StixData();

            if (root.TryGetProperty("objects", out var objects) && objects.ValueKind == JsonValueKind.Array)
            {
                foreach (var obj in objects.EnumerateArray())
                {
                    if (!obj.TryGetProperty("type", out var typeProp)) continue;

                    var type = typeProp.GetString();
                    var json = obj.GetRawText();

                    try
                    {
                        switch (type)
                        {
                            case "x-mitre-collection":
                            data.Collection = JsonSerializer.Deserialize<StixCollection>(json, options);
                            break;

                        case "attack-pattern":
                            data.AttackPatterns ??= new List<StixAttackPattern>();
                            data.AttackPatterns.Add(JsonSerializer.Deserialize<StixAttackPattern>(json, options));
                            break;

                        case "campaign":
                            data.Campaigns ??= new List<StixCampaign>();
                            data.Campaigns.Add(JsonSerializer.Deserialize<StixCampaign>(json, options));
                            break;

                        case "course-of-action":
                            data.CourseOfActions ??= new List<StixCourseOfAction>();
                            data.CourseOfActions.Add(JsonSerializer.Deserialize<StixCourseOfAction>(json, options));
                            break;

                        case "identity":
                            data.Identities ??= new List<StixIdentity>();
                            data.Identities.Add(JsonSerializer.Deserialize<StixIdentity>(json, options));
                            break;

                        case "intrusion-set":
                            data.IntrusionSets ??= new List<StixIntrusionSet>();
                            data.IntrusionSets.Add(JsonSerializer.Deserialize<StixIntrusionSet>(json, options));
                            break;

                        case "malware":
                            data.Malwares ??= new List<StixMalware>();
                            data.Malwares.Add(JsonSerializer.Deserialize<StixMalware>(json, options));
                            break;

                        case "relationship":
                            data.Relationships ??= new List<StixRelationship>();
                            data.Relationships.Add(JsonSerializer.Deserialize<StixRelationship>(json, options));
                            break;

                        case "tool":
                            data.Tools ??= new List<StixTool>();
                            data.Tools.Add(JsonSerializer.Deserialize<StixTool>(json, options));
                            break;

                        case "x-mitre-data-component":
                            data.DataComponents ??= new List<StixDataComponent>();
                            data.DataComponents.Add(JsonSerializer.Deserialize<StixDataComponent>(json, options));
                            break;

                        case "x-mitre-data-source":
                            data.DataSources ??= new List<StixDataSource>();
                            data.DataSources.Add(JsonSerializer.Deserialize<StixDataSource>(json, options));
                            break;

                        case "x-mitre-matrix":
                            data.Matrices ??= new List<StixMatrix>();
                            data.Matrices.Add(JsonSerializer.Deserialize<StixMatrix>(json, options));
                            break;

                        case "x-mitre-tactic":
                            data.Tactics ??= new List<StixTactic>();
                            data.Tactics.Add(JsonSerializer.Deserialize<StixTactic>(json, options));
                            break;

                        case "x-mitre-asset":
                            data.Assets ??= new List<StixAsset>();
                            var asset = JsonSerializer.Deserialize<StixAsset>(json, options);
                            if (asset != null) data.Assets.Add(asset);
                            break;

                        default:
                            Console.WriteLine($"Unknown object type: {type}");
                            break;  
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing {type}: {ex.Message}");
                    }
                }
            }

            return data;
        }

        public override void Write(Utf8JsonWriter writer, StixData value, JsonSerializerOptions options)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject(); 
            writer.WriteStartArray("objects"); 

            
            if (value.Collection != null)
            {
                JsonSerializer.Serialize(writer, value.Collection, options);
            }

            SerializeList(writer, value.AttackPatterns, options);
            SerializeList(writer, value.Campaigns, options);
            SerializeList(writer, value.CourseOfActions, options);
            SerializeList(writer, value.Identities, options);
            SerializeList(writer, value.IntrusionSets, options);
            SerializeList(writer, value.Malwares, options);
            SerializeList(writer, value.Relationships, options);
            SerializeList(writer, value.Tools, options);
            SerializeList(writer, value.DataComponents, options);
            SerializeList(writer, value.DataSources, options);
            SerializeList(writer, value.Matrices, options);
            SerializeList(writer, value.Tactics, options);
            SerializeList(writer, value.Assets, options);

            writer.WriteEndArray(); 
            writer.WriteEndObject(); 
        }

        private static void SerializeList<T>(Utf8JsonWriter writer, List<T> list, JsonSerializerOptions options)
        {
            if (list == null) return;

            foreach (var item in list)
            {
                if (item != null)
                {
                    JsonSerializer.Serialize(writer, item, options);
                }
            }
        }
    }
}
