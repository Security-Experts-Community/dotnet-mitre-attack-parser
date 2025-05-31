using MitreAttackParser.Helpers;
using MitreAttackParser.Models;
using System.Text.Json;

namespace MitreAttackParser
{
    public class AttackRepository : TaxiiApiClient
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonSerializerOptionsoptions;
        public readonly string ServerAddress;
        public readonly double PooledConnectionLifetime;
        public readonly double PooledConnectionIdleTimeout;
        public readonly double Timeout;
        public Collection EnterpriseCollection;
        public Collection ICSCollection;
        public Collection MobileCollection;
        public AttackRepository(string serverAddress = "https://attack-taxii.mitre.org/api/v21", double pooledConnectionLifetime = 5, double pooledConnectionIdleTimeout = 2, double timeout = 5)
        {
            ServerAddress = serverAddress;
            PooledConnectionLifetime = pooledConnectionLifetime;
            PooledConnectionIdleTimeout = pooledConnectionIdleTimeout;
            Timeout = timeout;
            _httpClient = new HttpClient(new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(this.PooledConnectionLifetime),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(this.PooledConnectionIdleTimeout),
            })
            {
                Timeout = TimeSpan.FromMinutes(this.Timeout) 
            };
            _jsonSerializerOptionsoptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new StixDataConverter() }
            };         
        }
        public async Task<bool> CreateAsync()
        {
            try
            {
                var collectionNames = await GetCollectionId(_httpClient, _jsonSerializerOptionsoptions, $"{ServerAddress}/collections");
                EnterpriseCollection = new(_httpClient, _jsonSerializerOptionsoptions, $"{ServerAddress}/collections/{collectionNames[0]}/objects");
                ICSCollection =  new(_httpClient, _jsonSerializerOptionsoptions, $"{ServerAddress}/collections/{collectionNames[1]}/objects");
                MobileCollection = new(_httpClient, _jsonSerializerOptionsoptions, $"{ServerAddress}/collections/{collectionNames[2]}/objects");
                return await EnterpriseCollection.CreateAsync()&&
                      await ICSCollection.CreateAsync() &&
                      await MobileCollection.CreateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Attack Repository: {ex.Message}");
                throw;
            }
        }

    }
}
