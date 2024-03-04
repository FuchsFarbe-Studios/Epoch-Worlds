using EpochApp.Client.Shared;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    ///     World form component for creating and updated user worlds.
    /// </summary>
    public partial class WorldForm : RequestComponent<WorldDTO>
    {
        private List<MetaCategory> _categories = new List<MetaCategory>();
        private string _error = string.Empty;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private List<ISOLanguage> _languages = new List<ISOLanguage>();
        private bool _submitting = false;
        private List<MetaTemplate> _templates = new List<MetaTemplate>();

        [Inject] private ILookupService LookupService { get; set; }
        [Inject] private IWorldService WorldService { get; set; }

        /// <summary>
        ///     Determines if the form is in edit mode or create mode.
        /// </summary>
        [Parameter] public bool IsEditForm { get; set; } = false;

        /// <summary> The world id to edit. </summary>
        [Parameter] public Guid? WorldId { get; set; } = null!;

        /// <summary>
        ///     The world model to edit or create.
        /// </summary>
        [Parameter] public WorldDTO WorldModel { get; set; } = new WorldDTO
                                                               {
                                                                   CurrentWorldDate = new WorldDateDTO(),
                                                                   MetaData = new List<WorldMetaDTO>(),
                                                                   WorldArticles = new List<ArticleDTO>(),
                                                                   WorldTags = new List<WorldTagDTO>(),
                                                                   WorldFiles = new List<UserFileDTO>(),
                                                                   WorldGenres = new List<WorldGenreDTO>()
                                                               };


        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _languages = await LookupService.GetLanguagesAsync();
            _categories = await LookupService.GetMetaAsync();
            _templates = await LookupService.GetMetaTemplatesAsync();
            if (WorldModel.MetaData.Count < _templates.Count)
                foreach (var template in _templates)
                    WorldModel.MetaData.Add(new WorldMetaDTO
                                            {
                                                TemplateId = template.TemplateId,
                                                CategoryId = template.CategoryId
                                            });
            if (WorldId != null)
                await LoadWorldAsync();
        }

        private void AddTemplates(MetaCategory cat)
        {
            _templates.AddRange(cat.Templates);
            Logger.LogInformation($"Added {cat.Templates.Count} templates to the list.");
        }

        private async Task LoadWorldAsync()
        {
            var world = await WorldService.GetWorldAsync(WorldId!.Value);
            if (world.MetaData.Count < _templates.Count)
                foreach (var template in _templates)
                    world.MetaData.Add(new WorldMetaDTO
                                       {
                                           TemplateId = template.TemplateId,
                                           CategoryId = template.CategoryId
                                       });
            WorldModel = world;
        }

        private async Task OnWorldSubmit(EditContext arg)
        {
            _submitting = true;
            var world = arg.Model as WorldDTO;
            world.OwnerId = Auth.CurrentUser.UserID;

            if (arg.Validate())
                Logger.LogInformation("Form validation passed");
            else
            {
                _submitting = false;
                Logger.LogInformation("Form validation failed");
                return;
            }

            await Task.Delay(500);
            if (IsEditForm)
            {
                var updatedWorld = await WorldService.UpdateWorldAsync(world);
                if (updatedWorld != null)
                {
                    Logger.LogInformation("World updated successfully");
                    Nav.NavigateTo($"{NavRef.WorldNav.Edit}/{WorldId}");
                }
                else
                {
                    _error = "World update failed.";
                    Logger.LogError("World update failed");
                }
                _submitting = false;
            }
            else
            {
                var newWorld = await WorldService.CreateWorldAsync(world);
                if (newWorld != null)
                {
                    Logger.LogInformation("World created successfully");
                    Nav.NavigateTo($"{NavRef.WorldNav.Edit}/{newWorld.WorldId}");
                }
                else
                {
                    _error = "World creation failed.";
                    Logger.LogError("World creation failed");
                }
            }
            _submitting = false;
            await Task.CompletedTask;
        }
    }
}