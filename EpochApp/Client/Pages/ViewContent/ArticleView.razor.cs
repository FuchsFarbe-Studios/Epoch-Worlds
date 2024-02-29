using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

#pragma warning disable CS0414// Field is assigned but its value is never used

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays an article's data in a read-only format for public display.
    /// </summary>
    public partial class ArticleView
    {
        private ArticleDTO _article = null!;

        [Inject] private HttpClient Client { get; set; }

        /// <summary>
        ///     The world id related to the article.
        /// </summary>
        [Parameter] public string WorldId { get; set; }

        /// <summary>
        ///     The article id to display.
        /// </summary>
        [Parameter] public string ArticleId { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Guid.TryParse(WorldId, out var worldId) && Guid.TryParse(ArticleId, out var articleId))
            {
                var article = await Client.GetFromJsonAsync<ArticleDTO>($"api/v1/Articles/Article/{worldId}/{articleId}");
                if (article != null)
                    _article = article;
            }
        }
    }
}