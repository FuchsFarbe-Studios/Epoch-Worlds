using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays a world's data in a read-only format for public display.
    /// </summary>
    public partial class WorldView
    {
        private WorldDTO _world = null!;

        /// <summary>
        ///     The world id to display.
        /// </summary>
        [Parameter] public string WorldId { get; set; }

        [Inject] private IWorldService WorldService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Guid.TryParse(WorldId, out var gWorldId))
            {
                var world = await WorldService.GetWorldViewAsync(gWorldId);
                if (world != null)
                    _world = world;
            }
            await base.OnInitializedAsync();
        }
    }
}