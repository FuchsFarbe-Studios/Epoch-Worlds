// EpochWorlds
// ArticleService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using EpochApp.Shared;
using EpochApp.Shared.Articles;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{

    /// <summary>
    ///    Service for handling and modifying Article Data.
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly HttpClient _client;
        private readonly IWebAssemblyHostEnvironment _host;
        private readonly ILogger<IArticleService> _logger;

        /// <summary>
        ///    Constructor for ArticleService.
        /// </summary>
        /// <param name="logger"> The logger. </param>
        /// <param name="host"> The web assembly host environment. </param>
        /// <param name="client"> The http client. </param>
        public ArticleService(ILogger<ArticleService> logger, IWebAssemblyHostEnvironment host, HttpClient client)
        {
            _logger = logger;
            _host = host;
            _client = client;
            // _client = new HttpClient { BaseAddress = new Uri($"{_host.BaseAddress}") };
            _logger.LogInformation($"ArticleService created, base address: {_client.BaseAddress}");
        }

        /// <inheritdoc />
        public async Task<List<ArticleDTO>> GetArticlesAsync()
        {
            var articles = await _client.GetFromJsonAsync<List<ArticleDTO>>("api/v1/Articles");
            return await Task.FromResult(articles);
        }

        /// <inheritdoc />
        public async Task<List<ArticleDTO>> GetWorldArticlesAsync(Guid worldId)
        {
            var worldArticles = await _client.GetFromJsonAsync<List<ArticleDTO>>($"api/v1/Articles/WorldArticles?worldId={worldId}");
            return await Task.FromResult(worldArticles);
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> GetWorldArticleAsync(Guid worldId, Guid articleId)
        {
            var worldArticle = await _client.GetFromJsonAsync<ArticleDTO>($"api/v1/Articles/Article/{worldId}/{articleId}");
            return await Task.FromResult(worldArticle);
        }

        /// <inheritdoc />
        public async Task<List<ArticleDTO>> GetUserArticlesAsync(Guid userId)
        {
            var userArticles = await _client.GetFromJsonAsync<List<ArticleDTO>>($"api/v1/Articles/UserArticles?userId={userId}");
            return await Task.FromResult(userArticles);
        }

        /// <inheritdoc />
        public async Task<ArticleDTO> GetArticleByIdAsync(Guid articleId)
        {
            var article = await _client.GetFromJsonAsync<ArticleDTO>($"api/v1/Articles/Article/{articleId}");
            return await Task.FromResult(article);
        }

        /// <inheritdoc />
        public async Task<Article> CreateArticleAsync(ArticleEditDTO article)
        {
            var response = await _client.PostAsJsonAsync("api/v1/Articles", article);
            if (response.IsSuccessStatusCode)
            {
                var newArticle = await response.Content.ReadFromJsonAsync<Article>();
                _logger.LogInformation($"Created article {newArticle.ArticleId} - {newArticle.Title}");
                return await Task.FromResult(newArticle);
            }
            _logger.LogWarning("Failed to create article!");
            return null;
        }

        /// <inheritdoc />
        public async Task<ArticleEditDTO> UpdateArticleAsync(ArticleEditDTO article, Guid articleId, Guid userId)
        {
            var response = await _client.PutAsJsonAsync($"api/v1/Articles?userId={userId}&articleId={articleId}", article);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Updated article {articleId} - {article.Title}");
                return await Task.FromResult(article);
            }
            _logger.LogWarning("Failed to update article!");
            return null;
        }

        /// <inheritdoc />
        public async Task<ArticleEditDTO> GetEditArticleAsync(Guid articleId)
        {
            var article = await _client.GetFromJsonAsync<ArticleEditDTO>($"api/v1/Articles/Article/Edited/{articleId}");
            return await Task.FromResult(article);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ArticleTemplateDTO>> GetArticleTemplatesAsync()
        {
            var templates = await _client.GetFromJsonAsync<IEnumerable<ArticleTemplateDTO>>("api/v1/Articles/Templates");
            return await Task.FromResult(templates);
        }

        /// <inheritdoc />
        public async Task<ArticleTemplateDTO> GetArticleTemplateAsync(int categoryId)
        {
            var template = await _client.GetFromJsonAsync<ArticleTemplateDTO>($"api/v1/Articles/Template/{categoryId}");
            return await Task.FromResult(template);
        }

        /// <inheritdoc />
        public async Task<ArticleTemplateDTO> CreateArticleTemplateAsync(ArticleTemplateDTO template)
        {
            var response = await _client.PostAsJsonAsync("api/v1/Articles/Template", template);
            if (response.IsSuccessStatusCode)
            {
                var newTemplate = await response.Content.ReadFromJsonAsync<ArticleTemplateDTO>();
                _logger.LogInformation($"Created template {newTemplate.CategoryId} - {newTemplate.TemplateName}");
                return await Task.FromResult(newTemplate);
            }
            _logger.LogWarning("Failed to create template!");
            return null;
        }

        /// <inheritdoc />
        public async Task<ArticleTemplateDTO> UpdateArticleTemplateAsync(ArticleTemplateDTO template)
        {
            var response = await _client.PutAsJsonAsync("api/v1/Articles/Template", template);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Updated template {template.CategoryId} - {template.TemplateName}");
                return await Task.FromResult(template);
            }
            _logger.LogWarning("Failed to update template!");
            return null;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteArticleTemplateAsync(int templateId)
        {
            var response = await _client.DeleteAsync($"api/v1/Articles/Template/{templateId}");
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Deleted template {templateId}");
                return await Task.FromResult(true);
            }
            _logger.LogWarning("Failed to delete template!");
            return await Task.FromResult(false);
        }
    }
}