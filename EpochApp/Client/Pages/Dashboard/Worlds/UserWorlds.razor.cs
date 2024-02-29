using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    ///     Displays a list of the user's worlds.
    /// </summary>
    public partial class UserWorlds
    {
        private List<UserWorldDTO> _newUserWorlds = new List<UserWorldDTO>();

        /// <summary> The active world. </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        /// <summary> The new active world. </summary>
        [CascadingParameter] protected UserWorldDTO NewActiveWorld { get; set; }
        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private NavigationManager Nav { get; set; }

        [Inject] private IDialogService DialogService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var newWorlds = await Client.GetFromJsonAsync<List<UserWorldDTO>>($"api/v2/Worlds/UserWorlds?userId={Auth.CurrentUser.UserID}");
            if (newWorlds.Any())
                _newUserWorlds.AddRange(newWorlds);
            await base.OnInitializedAsync();
        }

        private async Task HandleWorldDeletionAsync(UserWorldDTO world)
        {
            if (_newUserWorlds.Count < 2)
            {
                await DialogService.ShowMessageBox(
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

            var response = await Client.DeleteFromJsonAsync<UserWorldDTO>($"api/v2/Worlds?userId={Auth.CurrentUser.UserID}&worldId={world.WorldId}");
            if (response != null)
            {
                if (_newUserWorlds.Any(x => x.WorldId == response.WorldId))
                {
                    _newUserWorlds.Remove(response);
                    StateHasChanged();
                }
            }
        }
    }
}