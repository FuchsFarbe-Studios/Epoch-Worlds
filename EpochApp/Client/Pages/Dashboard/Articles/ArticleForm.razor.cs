using EpochApp.Client.Shared;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

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
        private ArticleTemplateDTO _template = null!;
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
        [CascadingParameter] protected UserWorldDTO ActiveWorld { get; set; }

        [Inject] private ILookupService LookupService { get; set; }

        [Inject] private IArticleService ArticleService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var cats = await LookupService.GetArticleCategoriesAsync();
            if (cats != null || cats.Any())
                _categories = cats;
            if (ArticleEdit == null || !IsEditMode)
            {
                Model = new ArticleEditDTO
                        {
                            WorldId = ActiveWorld?.WorldId,
                            AuthorId = Auth?.CurrentUser?.UserID,
                            Sections = new List<SectionEditDTO>(),
                            CategoryId = _categories.FirstOrDefault().CategoryID
                        };
            }
            else
            {
                Model = ArticleEdit;
            }
            await base.OnInitializedAsync();
        }

        private async Task OnArticleSubmit(EditContext ctx)
        {
            var article = ctx.Model as ArticleEditDTO;
            article.WorldId = ActiveWorld?.WorldId;
            article.AuthorId = Auth?.CurrentUser?.UserID;

            if (IsEditMode)
            {
                var updatedArticle = await ArticleService.UpdateArticleAsync(article, Model.ArticleId ?? Guid.Empty, Auth?.CurrentUser?.UserID ?? Guid.Empty);
                if (updatedArticle == null)
                    Logger.LogError("Failed to update article!");
                else
                    Nav.NavigateTo($"{NavRef.ArticleNav.Edit}/{updatedArticle?.ArticleId}");
            }
            else
            {
                var newArticle = await ArticleService.CreateArticleAsync(article);
                if (newArticle != null)
                {
                    Logger.LogInformation("Article created successfully!");
                    Nav.NavigateTo($"{NavRef.ArticleNav.Edit}/{newArticle.ArticleId}");
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

        private async Task GenerateArticleTemplateAsync()
        {
            var template = await ArticleService.GetArticleTemplateAsync(Model.CategoryId);
            if (template == null)
                await Task.CompletedTask;
            else
            {
                _template = template;
                _activePanelIndex = 1;
                StateHasChanged();
            }
        }
    }
}