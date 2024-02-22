using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays a world's data in a read-only format for public display.
    /// </summary>
    public partial class WorldView
    {
        private WorldDTO _world;
        [Parameter] public string WorldId { get; set; }
    }
}