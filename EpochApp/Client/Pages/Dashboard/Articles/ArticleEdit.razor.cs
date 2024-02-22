using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     Allows a user to edit an article or select an article to edit.
    /// </summary>
    public partial class ArticleEdit
    {
        private ArticleEditDTO _editArticle;
        [Parameter] public string ArticleId { get; set; }

        [Inject] private HttpClient Client { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!string.IsNullOrEmpty(ArticleId))
            {
                var article = await Client.GetFromJsonAsync<ArticleEditDTO>($"api/v1/Articles/Article/Edited/{ArticleId}");
                if (article != null)
                    _editArticle = article;
            }
        }
    }
}