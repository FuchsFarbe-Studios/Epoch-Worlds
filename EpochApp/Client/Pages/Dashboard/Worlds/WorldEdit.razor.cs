using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    ///     Allows a user to select a world to edit, or opens the world edit form.
    /// </summary>
    public partial class WorldEdit
    {
        private WorldDTO _selectedWorld;
        private List<WorldDTO> _userWorlds = new List<WorldDTO>();
        /// <summary>
        ///     The ID of the world to edit.
        /// </summary>
        [Parameter] public string WorldId { get; set; }

        [Inject] private IWorldService WorldService { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (string.IsNullOrWhiteSpace(WorldId))
            {
                var worlds = await WorldService.GetUserWorldsAsync(Auth.CurrentUser.UserID);
                if (worlds != null && worlds.Count != 0)
                    _userWorlds.AddRange(worlds);
            }

            if (Guid.TryParse(WorldId, out var worldId))
                _selectedWorld = await WorldService.GetWorldAsync(worldId);
        }

        /// <inheritdoc />
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (!string.IsNullOrWhiteSpace(WorldId))
                if (Guid.TryParse(WorldId, out var worldId))
                    _selectedWorld = await WorldService.GetWorldAsync(worldId);
        }
    }
}