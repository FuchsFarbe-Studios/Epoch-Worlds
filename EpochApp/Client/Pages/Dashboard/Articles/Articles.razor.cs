using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     Component for displaying articles from the users active world.
    /// </summary>
    public partial class Articles
    {
        private List<ArticleCategory> _categories = new List<ArticleCategory>();
        private ArticleCategory _filterCategory = null!;
        private List<ArticleDTO> _filteredArticles = new List<ArticleDTO>();
        private List<ArticleDTO> _worldArticles = new List<ArticleDTO>();

        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ILookupService LookupService { get; set; }
        [Inject] private IArticleService ArticleService { get; set; }

        /// <summary> The active world. </summary>
        [CascadingParameter] protected UserWorldDTO ActiveWorld { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (ActiveWorld != null)
            {
                var categories = await LookupService.GetArticleCategoriesAsync();
                if (categories.Any())
                    _categories = categories;
                await RefreshWorldArticlesAsync();
            }
            await base.OnInitializedAsync();
        }

        private async Task RefreshWorldArticlesAsync()
        {
            var articles = await ArticleService.GetWorldArticlesAsync(ActiveWorld.WorldId);
            _worldArticles = articles;
            _filteredArticles = _worldArticles;
        }

        private async Task FilterArticlesByCategory(ArticleCategory cat)
        {
            _filterCategory = cat;
            if (cat != null)
                _filteredArticles = _worldArticles.Where(a => a.CategoryId == cat.CategoryID).ToList();
            else
                _filteredArticles = _worldArticles;
            StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task AttemptArticleDeleteAsync(ArticleDTO article)
        {
            bool? result = await DialogService.ShowMessageBox(
                           "Deleting Article",
                           $"You are about to delete the article: {article.Title}. Are you sure?",
                           yesText: "Delete!", cancelText: "I've Changed my mind!");
            if (result == true)
            {
                await ArticleService.DeleteArticleAsync(Auth.CurrentUser.UserID, article.ArticleId);
                await RefreshWorldArticlesAsync();
                StateHasChanged();
            }
            return;
        }

        private async Task ToggleArticlePublishAsync(ArticleDTO article)
        {
            await Task.CompletedTask;
        }
    }
}