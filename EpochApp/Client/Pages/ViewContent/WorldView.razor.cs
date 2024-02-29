using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays a world's data in a read-only format for public display.
    /// </summary>
    public partial class WorldView
    {
        private World _world = null!;

        /// <summary>
        ///     The world id to display.
        /// </summary>
        [Parameter] public string WorldId { get; set; }

        [Inject] private HttpClient Client { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Guid.TryParse(WorldId, out var gWorldId))
            {
                var world = await Client.GetFromJsonAsync<World>($"api/v2/Worlds/View/{gWorldId}");
                if (world != null)
                    _world = world;
            }
            await base.OnInitializedAsync();
        }
    }
}