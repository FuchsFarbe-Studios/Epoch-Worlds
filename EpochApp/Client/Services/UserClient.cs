// EpochWorlds
// UserClient.cs
// FuchsFarbe Studios 2023
// matsu
// Modified: 23-12-2023
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    public class UserClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly ILogger<UserClient> _logger;

        public UserClient(HttpClient client, IConfiguration config, ILogger<UserClient> logger)
        {
            _config = config;
            _logger = logger;
            _client.BaseAddress = new Uri($"{client.BaseAddress}{_config.GetSection("Api").GetSection("UserUri").Value}");
            _logger.LogInformation("User URI: " + _client.BaseAddress);
            _client = client;
        }

        public UserClient()
        {
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string requestUri, TValue value)
        {
            return await _client.PostAsJsonAsync<TValue>(requestUri, value);
        }
    }
}