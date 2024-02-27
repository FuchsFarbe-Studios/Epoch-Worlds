using EpochApp.Client.Shared;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    ///     World form component for creating and updated user worlds.
    /// </summary>
    public partial class NewWorldForm : RequestComponent<UserWorldDTO>
    {
        private List<MetaCategory> _categories = new List<MetaCategory>();
        private string _error = string.Empty;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private List<ISOLanguage> _languages = new List<ISOLanguage>();
        private bool _submitting = false;
        private List<MetaTemplate> _templates = new List<MetaTemplate>();

        [Parameter] public bool IsEditForm { get; set; } = false;

        [Parameter] public Guid? WorldId { get; set; } = null!;

        [Parameter] public UserWorldDTO WorldModel { get; set; } = new UserWorldDTO
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
            _languages = await Client.GetFromJsonAsync<List<ISOLanguage>>("api/v1/Lookups/lkLanguages");
            _categories = await Client.GetFromJsonAsync<List<MetaCategory>>("api/v1/Lookups/lkMeta");
            _templates = await Client.GetFromJsonAsync<List<MetaTemplate>>("api/v1/Lookups/lkMetaTemplates");
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
            var world = await Client.GetFromJsonAsync<UserWorldDTO>($"api/v2/Worlds/{WorldId}");
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
            var world = arg.Model as UserWorldDTO;
            world.OwnerId = Auth.CurrentUser.UserID;

            if (arg.Validate())
                Logger.LogInformation("Form validation passed.");
            else
            {
                _submitting = false;
                Logger.LogInformation("Form validation failed.");
                return;
            }

            await Task.Delay(500);
            if (IsEditForm)
            {
                var response = await Client.PutAsJsonAsync($"api/v2/Worlds/{WorldId}", world);
                if (response.IsSuccessStatusCode)
                {
                    Logger.LogInformation("World updated successfully.");
                    Nav.NavigateTo($"{NavRef.WorldNav.Edit}/{WorldId}");
                }
                else
                {
                    _error = "World update failed.";
                    _error = await response.Content.ReadAsStringAsync();
                    Logger.LogError("World update failed.");
                }
                _submitting = false;
            }
            else
            {
                var response = await Client.PostAsJsonAsync("api/v2/Worlds", world);
                if (response.IsSuccessStatusCode)
                {
                    var newWorld = await response.Content.ReadFromJsonAsync<UserWorldDTO>();
                    Nav.NavigateTo($"{NavRef.WorldNav.Edit}/{newWorld.WorldId}");
                }
                else
                {
                    _error = "World creation failed.";
                    _error = await response.Content.ReadAsStringAsync();
                    Logger.LogError("World creation failed.");
                }
            }
            _submitting = false;
            await Task.CompletedTask;
        }
    }
}