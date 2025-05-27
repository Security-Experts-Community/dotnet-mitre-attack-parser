using MitreAttackParser.Entities;
using System.Text.Json;

namespace MitreAttackParser.Helpers
{
    public interface ITaxiiApi
    {
        Task<List<string>> GetCollectionId(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptionsoptions, string url);
        public Task<StixData> GetAllCollectionObjects(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptionsoptions, string url);
        Task<string> SendResponseAsync(HttpClient httpClient, string url);
    }
}
