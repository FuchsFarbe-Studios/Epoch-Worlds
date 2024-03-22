// EpochWorlds
// SlugService.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
using EpochApp.Shared;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    #pragma warning disable CS1591
    public class SlugService : ISlugService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ISlugService> _logger;

        public SlugService(HttpClient client, ILogger<SlugService> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<WorldDTO> GetWorldBySlugAsync(string slug)
        {
            var world = await _client.GetFromJsonAsync<WorldDTO>($"api/v1/Slugs/World/{slug}");
            return await Task.FromResult(world);
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> GetArticleBySlugAsync(string slug)
        {
            var article = await _client.GetFromJsonAsync<ArticleDTO>($"api/v1/Slugs/Article/{slug}");
            return await Task.FromResult(article);
        }
    }
}