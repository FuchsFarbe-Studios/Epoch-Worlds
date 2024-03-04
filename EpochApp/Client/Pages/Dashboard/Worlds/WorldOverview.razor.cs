using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    ///     A world overview and preview page.
    /// </summary>
    public partial class WorldOverview
    {
        /// <summary>
        ///     The currently active world.
        /// </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // Get articles

            // Get world data

            // Get world events
        }
    }
}