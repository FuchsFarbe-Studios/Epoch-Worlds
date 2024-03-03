using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     Component for displaying articles from the users active world.
    /// </summary>
    public partial class Articles
    {
        private List<ArticleDTO> _worldArticles { get; set; } = new List<ArticleDTO>();

        [Inject] private IArticleService ArticleService { get; set; }

        /// <summary> The active world. </summary>
        [CascadingParameter] protected UserWorldDTO ActiveWorld { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (ActiveWorld != null)
            {
                var articles = await ArticleService.GetWorldArticlesAsync(ActiveWorld.WorldId);
                if (articles.Any())
                    _worldArticles = articles;
            }
            await base.OnInitializedAsync();
        }
    }
}