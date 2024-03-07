// EpochWorlds
// BuilderService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using EpochApp.Shared;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    /// <inheritdoc />
    public class BuilderService : IBuilderService
    {
        private readonly HttpClient _client;
        private readonly ILogger<IBuilderService> _logger;

        /// <summary>
        /// Service that provides operations for managing builder content.
        /// </summary>
        public BuilderService(HttpClient client, ILogger<BuilderService> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BuilderContentDTO>> GetBuilderContentByAuthorAsync(Guid userId)
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<BuilderContentDTO>>($"api/v1/Builders/ContentByAuthor/{userId}");
            return response;
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> GetBuilderContentByIdAsync(Guid contentId)
        {
            var response = await _client.GetFromJsonAsync<BuilderContentDTO>($"api/v1/Builders/Content/{contentId}");
            return response;
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> GenerateContentAsync(Guid contentId, Guid userId)
        {
            var response = await _client.PostAsJsonAsync($"api/v1/Builders/GenerateContent/{contentId}/{userId}", new {});
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to generate content for builder with ID {contentId} and user ID {userId}.");
                return null;
            }
            var content = await response.Content.ReadFromJsonAsync<BuilderContentDTO>();
            return content;
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> CreateNewBuilderContentAsync(BuilderContentDTO content)
        {
            var response = await _client.PostAsJsonAsync("api/v1/Builders/CreateContent", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to create new builder content.");
                return null;
            }
            var newContent = await response.Content.ReadFromJsonAsync<BuilderContentDTO>();
            return newContent;
        }

        /// <inheritdoc />
        public async Task<BuilderContentDTO> UpdateBuilderAsync(Guid userId, Guid contentId, BuilderContentDTO content)
        {
            var response = await _client.PutAsJsonAsync($"api/v1/Builders/UpdateContent/{userId}/{contentId}", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to update builder content with ID {contentId}.");
                return null;
            }
            var updatedContent = await response.Content.ReadFromJsonAsync<BuilderContentDTO>();
            return updatedContent;
        }
    }
}