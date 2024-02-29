using EpochApp.Client.Shared;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Articles
{
    /// <summary>
    ///     Component for creating and editing articles.
    /// </summary>
    public partial class ArticleForm
    {
        private int _activePanelIndex;
        private List<ArticleCategory> _categories = new List<ArticleCategory>();
        private MudDynamicTabs _sectionTabs;
        private EpochValidator _validator;

        /// <summary>
        ///     Article edit information.
        /// </summary>
        [Parameter] public ArticleEditDTO ArticleEdit { get; set; } = null!;

        /// <summary>
        ///     Determines if the form is in edit mode or create mode.
        /// </summary>
        [Parameter] public bool IsEditMode { get; set; }

        /// <summary> The active world. </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var cats = await Client.GetFromJsonAsync<List<ArticleCategory>>("api/v1/Articles/Categories");
            if (cats != null || cats.Any())
                _categories = cats;

            if (ArticleEdit == null)
            {
                Model = new ArticleEditDTO
                        {
                            WorldId = ActiveWorld?.WorldID,
                            AuthorId = Auth?.CurrentUser?.UserID,
                            Sections = new List<SectionEditDTO>()
                        };
            }
            else
            {
                Model = ArticleEdit;
            }
        }

        private async Task OnArticleSubmit(EditContext ctx)
        {
            var article = ctx.Model as ArticleEditDTO;
            article.WorldId = ActiveWorld?.WorldID;
            article.AuthorId = Auth?.CurrentUser?.UserID;

            if (IsEditMode)
            {
                var response = await Client.PutAsJsonAsync($"api/v1/Articles?userId={Auth?.CurrentUser?.UserID}&articleId={Model.ArticleId}", Model);
                if (!response.IsSuccessStatusCode)
                    Logger.LogError("Failed to update article!");
                var content = await response.Content.ReadFromJsonAsync<ArticleDTO>();
                Nav.NavigateTo($"{NavRef.ArticleNav.Edit}/{content.ArticleId}");
            }
            else
            {
                var response = await Client.PostAsJsonAsync<ArticleEditDTO>("api/v1/Articles", Model);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<ArticleDTO>();
                    Logger.LogInformation("Article created successfully!");
                    Nav.NavigateTo($"{NavRef.ArticleNav.Edit}/{content.ArticleId}");
                }
                else
                {
                    Logger.LogError("Failed to create article!");
                }
            }
        }

        private Task AddArticleSectionAsync(MouseEventArgs arg)
        {
            Model.Sections.Add(new SectionEditDTO
                               {
                                   Title = "New Section"
                               });
            StateHasChanged();
            return Task.CompletedTask;
        }

        private async Task DeleteSectionAsync(SectionEditDTO section)
        {
            if (Model.Sections.Contains(section))
            {
                Model.Sections.Remove(section);
                StateHasChanged();
            }
            await Task.CompletedTask;
        }
    }
}