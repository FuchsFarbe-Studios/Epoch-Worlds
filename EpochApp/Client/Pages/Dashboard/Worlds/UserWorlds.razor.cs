using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    ///     Displays a list of the user's worlds.
    /// </summary>
    public partial class UserWorlds
    {
        private List<WorldDTO> _newUserWorlds = new List<WorldDTO>();

        /// <summary> The active world. </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        /// <summary> The new active world. </summary>
        [CascadingParameter] protected WorldDTO NewActiveWorld { get; set; }

        [Inject] private IWorldService Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private NavigationManager Nav { get; set; }

        [Inject] private IDialogService DialogService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var newWorlds = await Client.GetUserWorldsAsync(Auth.CurrentUser.UserID);
            if (newWorlds.Any())
                _newUserWorlds.AddRange(newWorlds);
            await base.OnInitializedAsync();
        }

        private async Task HandleWorldDeletionAsync(WorldDTO world)
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

            var deletedWorld = await Client.DeleteWorldAsync(Auth.CurrentUser.UserID, world.WorldId);
            if (_newUserWorlds.Any(x => x.WorldId == deletedWorld.WorldId))
            {
                _newUserWorlds.Remove(deletedWorld);
                StateHasChanged();
            }
        }
    }
}