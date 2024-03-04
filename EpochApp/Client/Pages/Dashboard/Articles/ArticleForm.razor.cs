using EpochApp.Shared;
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
        private List<SectionDTO> _articleSections = new List<SectionDTO>();
        private List<ArticleCategory> _categories = new List<ArticleCategory>();
        private MudDynamicTabs _sectionTabs;
        private ArticleTemplateDTO _template = null!;
        private List<SectionTemplateDTO> _templateSections = new List<SectionTemplateDTO>();

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

        [Inject] private ILookupService LookupService { get; set; }

        [Inject] private IArticleService ArticleService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var cats = await LookupService.GetArticleCategoriesAsync();
            if (cats != null || cats.Any())
                _categories = cats;
            _template = await ArticleService.GetArticleTemplateAsync(Model.CategoryId);
            if (_template != null)
                _templateSections = _template.Sections.ToList();
            if (ArticleEdit == null || !IsEditMode)
            {
                Model = new ArticleEditDTO
                        {

                            WorldId = ActiveWorld?.WorldId,
                            AuthorId = Auth?.CurrentUser?.UserID,
                            DisplayAuthor = true,
                            ShowInTableOfContents = true,
                            ShowTableOfContents = true,
                            Sections = new List<SectionEditDTO>(),
                            CategoryId = _categories.FirstOrDefault().CategoryID,
                            Header = new ArticleHeaderDTO(),
                            SideBar = new SideBarDTO(),
                            Footer = new ArticleFooterDTO(),
                        };
            }
            else
            {
                Model = ArticleEdit;
                if (Model.Header == null)
                    Model.Header = new ArticleHeaderDTO()
                                   {
                                       ArticleId = Model.ArticleId
                                   };
                if (Model.SideBar == null)
                    Model.SideBar = new SideBarDTO()
                                    {
                                        ArticleId = Model.ArticleId
                                    };
                if (Model.Footer == null)
                    Model.Footer = new ArticleFooterDTO()
                                   {
                                       ArticleId = Model.ArticleId
                                   };
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
                var updatedArticle = await ArticleService.UpdateArticleAsync(article, Model.ArticleId, Auth?.CurrentUser?.UserID ?? Guid.Empty);
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
                _templateSections = template.Sections.ToList();
                Model.Sections = new List<SectionEditDTO>();
                foreach (var section in _templateSections)
                {
                    Model.Sections.Add(new SectionEditDTO
                                       {
                                           Title = section.SectionName,
                                           Content = section.Placeholder
                                       });
                }
                StateHasChanged();
            }
        }
    }
}