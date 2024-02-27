using EpochApp.Client.Shared;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    public partial class NewWorldForm : RequestComponent<UserWorldDTO>
    {
        private List<ISOLanguage> _languages = new List<ISOLanguage>();
        private bool _submitting = false;
        [Parameter] public Guid? WorldId { get; set; } = null!;
        [Parameter] public UserWorldDTO WorldModel { get; set; } = new UserWorldDTO();


        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _languages = await Client.GetFromJsonAsync<List<ISOLanguage>>("api/v1/Lookups/lkLanguages");
            if (WorldId != null)
                await LoadWorldAsync();
        }

        private async Task LoadWorldAsync()
        {
            var world = await Client.GetFromJsonAsync<UserWorldDTO>($"api/v2/Worlds/{WorldId}");
            WorldModel = world;
        }

        private Task OnWorldSubmit(EditContext arg)
        {
            _submitting = true;
            if (arg.Validate())
            {
                Logger.LogInformation("Form validation passed.");
            }
            else
            {
                _submitting = false;
                Logger.LogInformation("Form validation failed.");
                return Task.CompletedTask;
            }
            Logger.LogInformation("Submitting world form...");
            var world = arg.Model as NewWorldDTO;
            return Task.CompletedTask;
        }
    }
}