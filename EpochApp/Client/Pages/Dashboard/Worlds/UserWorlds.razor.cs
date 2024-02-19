using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    public partial class UserWorlds
    {
        /// <summary> The active world. </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        private List<WorldDTO> _userWorlds = new List<WorldDTO>();
        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private NavigationManager Nav { get; set; }

        [Inject] private IDialogService DialogService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var worlds = await Client.GetFromJsonAsync<List<WorldDTO>>($"api/v1/Worlds/User?ownerId={Auth.CurrentUser.UserID}");
            if (worlds.Any())
                _userWorlds.AddRange(worlds);
        }

        private async Task HandleWorldDeletionAsync(WorldDTO world)
        {
            if (_userWorlds.Count < 2)
            {
                var messageResult = await DialogService.ShowMessageBox(
                                    "Warning - Cannot Delete Only World!",
                                    $"You cannot delete the world of {world.WorldName}. This is your only world! You must create a new world before you can delete this one.",
                                    "Okay", cancelText: "Cancel");
                return;
            }

            var result = await DialogService.ShowMessageBox(
                         "Warning - Deleting World!",
                         $"You are about to delete the world of {world.WorldName}. This action cannot be undone! Are you sure you want to proceed?",
                         "Yes! Delete World!", cancelText: "Wait! I've changed my mind!");
            if (result != true)
                return;

            var response = await Client.DeleteFromJsonAsync<WorldDTO>($"api/v1/Worlds/{Auth.CurrentUser.UserID}/{world.WorldID}");
            if (response != null)
            {
                if (_userWorlds.Any(x => x.WorldID == response.WorldID))
                {
                    _userWorlds.Remove(response);
                    StateHasChanged();
                }
            }
        }
    }
}