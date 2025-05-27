using MitreAttackParser.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MitreAttackParser.Helpers
{
    public class TaxiiApi : ITaxiiApi
    {
        private struct StixCollectionInfo()
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("can_read")]
            public bool CanRead { get; set; }

            [JsonPropertyName("can_write")]
            public bool CanWrite { get; set; }

            [JsonPropertyName("media_types")]
            public List<string> MediaTypes { get; set; } = new List<string>();
        }

        public async Task<List<string>> GetCollectionId(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptionsoptions, string url)
        {
            try
            {
                var root = JsonSerializer.Deserialize<Dictionary<string, List<StixCollectionInfo>>>(await SendResponseAsync(httpClient, url), jsonSerializerOptionsoptions);
                return (from collectionInfo in root["collections"]
                        select collectionInfo.Id).ToList();    
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserialization collection names: {ex.Message}");
                throw;
            }
        }

        public async Task<StixData> GetAllCollectionObjects(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptionsoptions, string url)
        {
            try
            {
                var response = JsonSerializer.Deserialize<StixData> (await SendResponseAsync(httpClient, url), jsonSerializerOptionsoptions) ?? throw new InvalidOperationException("Response is null");
                return response;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserialization collection objects: {ex.Message}");
                throw;
            }
        }

        public async Task<string> SendResponseAsync(HttpClient httpClient, string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/taxii+json; version=2.1");
                HttpResponseMessage response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Error: {ex.Message}");
                throw;
            }
        }
    }
}
