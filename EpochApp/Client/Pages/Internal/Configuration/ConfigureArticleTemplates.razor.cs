using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Pages.Internal.Configuration
{
    public partial class ConfigureArticleTemplates
    {
        private List<ArticleTemplateDTO> _articleTemplates = new List<ArticleTemplateDTO>();
        private List<ArticleCategory> _categories = new List<ArticleCategory>();
        private ArticleTemplateDTO _templateModel = new ArticleTemplateDTO();

        [Inject] private ILookupService LookupService { get; set; }

        [Inject] private IArticleService ArticleService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var categories = await LookupService.GetArticleCategoriesAsync();
            _categories = categories;
            _templateModel.CategoryId = _categories.FirstOrDefault()?.CategoryID ?? 0;
            await RefreshTemplatesAsync();
            await base.OnInitializedAsync();
        }

        private async Task RefreshTemplatesAsync()
        {
            var templates = await ArticleService.GetArticleTemplatesAsync();
            _articleTemplates = templates.ToList();
        }

        private Task OnEditClicked(ArticleTemplateDTO contextItem)
        {
            _templateModel = contextItem;
            StateHasChanged();
            return Task.CompletedTask;
        }

        private async Task AddNewTemplateAsync(EditContext arg)
        {
            var template = arg.Model as ArticleTemplateDTO;
            var savedTemplate = await ArticleService.CreateArticleTemplateAsync(template);
            _templateModel = new ArticleTemplateDTO
                             {
                                 TemplateId = 0,
                                 CategoryId = _categories.FirstOrDefault()?.CategoryID ?? 0,
                                 TemplateName = "New Template",
                                 Sections = new List<SectionTemplateDTO>(),
                                 Meta = new List<ArticleMetaDTO>()
                             };
            await RefreshTemplatesAsync();
            StateHasChanged();
        }

        private Task AddSection()
        {
            _templateModel.Sections.Add(new SectionTemplateDTO
                                        {
                                            SectionName = "New Section",
                                        });
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task AddMeta()
        {
            _templateModel.Meta.Add(new ArticleMetaDTO
                                    {
                                        MetaName = "New Meta",
                                        Description = null,
                                        Type = FieldType.Text,
                                        Placeholder = null,
                                        HelpText = null
                                    });
            StateHasChanged();
            return Task.CompletedTask;
        }

        private async Task DeleteSectionAsync(SectionTemplateDTO sec)
        {
            _templateModel.Sections.Remove(sec);
            StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task DeleteMetaAsync(ArticleMetaDTO meta)
        {
            _templateModel.Meta.Remove(meta);
            StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task UpdateTemplateAsync(EditContext arg)
        {
            var template = arg.Model as ArticleTemplateDTO;
            var updatedTemplate = await ArticleService.UpdateArticleTemplateAsync(template);
            StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task ResetModelAsync()
        {
            _templateModel = new ArticleTemplateDTO
                             {
                                 CategoryId = _categories.FirstOrDefault()?.CategoryID ?? 0,
                                 TemplateName = "New Template",
                                 Sections = new List<SectionTemplateDTO>(),
                                 Meta = new List<ArticleMetaDTO>()
                             };
            StateHasChanged();
            await Task.CompletedTask;
        }
    }
}