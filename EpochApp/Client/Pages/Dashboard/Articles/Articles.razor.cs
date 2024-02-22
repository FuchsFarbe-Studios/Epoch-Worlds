using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     Component for displaying articles from the users active world.
    /// </summary>
    public partial class Articles
    {
        private List<ArticleDTO> _worldArticles { get; set; } = new List<ArticleDTO>();

        [Inject] private HttpClient Client { get; set; }

        /// <summary> The active world. </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (ActiveWorld != null)
            {
                var articles = await Client.GetFromJsonAsync<List<ArticleDTO>>($"api/v1/Articles/WorldArticles?worldId={ActiveWorld.WorldID}");
                if (articles.Any())
                    _worldArticles = articles;
            }
        }
    }
}