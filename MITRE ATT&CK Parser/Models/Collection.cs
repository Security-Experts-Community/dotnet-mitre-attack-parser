using MitreAttackParser.Entities;
using MitreAttackParser.Helpers;
using System.Text.Json;

namespace MitreAttackParser.Models
{
    public class Collection : TaxiiApi
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonSerializerOptionsoptions;
        private string _url;
        private StixCollection _aboutCollection = new();
        public StixCollection AboutCollection() => _aboutCollection;
        public List<StixAttackPattern> Techniques { get; set; }
        public List<StixCampaign> Campaigns { get; set; }
        public List<StixCourseOfAction> CourseOfActions { get; set; }
        public List<StixIdentity> Identities { get; set; }
        public List<StixIntrusionSet> IntrusionSets{ get; set; }
        public List<StixMalware> Malwares { get; set; }
        public List<StixRelationship> Relationships{ get; set; }
        public List<StixTool> Tools{ get; set; }
        public List<StixDataComponent> DataComponents { get; set; }
        public List<StixDataSource> DataSources { get; set; }
        public List<StixMatrix> Matrices { get; set; }
        public List<StixTactic> Tactics{ get; set; }
        public List<StixAsset>? Assets { get; set; }

        public Collection(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptionsoptions, string url)
        { 
            _httpClient = httpClient;
            _jsonSerializerOptionsoptions = jsonSerializerOptionsoptions;
            _url = url;
        }
        public async Task<bool> CreateAsync()
        {
            try
            {
                var objects = await GetAllCollectionObjects(_httpClient, _jsonSerializerOptionsoptions, _url);
                _aboutCollection = objects.Collection;
                Techniques = objects.AttackPatterns;
                Campaigns = objects.Campaigns;
                CourseOfActions = objects.CourseOfActions;
                Identities = objects.Identities;
                IntrusionSets = objects.IntrusionSets;
                Malwares = objects.Malwares;
                Relationships = objects.Relationships;
                Tools = objects.Tools;
                DataComponents = objects.DataComponents;
                DataSources = objects.DataSources;
                Matrices = objects.Matrices;
                Tactics = objects.Tactics;
                Assets = objects.Assets;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating collection: {ex.Message}");
                throw;
            }
        }
    }
}
