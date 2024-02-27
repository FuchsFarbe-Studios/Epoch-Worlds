using System.Net.Http.Json;
using EpochApp.Shared;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     Displays a world's data in a read-only format for public display.
    /// </summary>
    public partial class WorldView
    {
        private World _world;
        [Parameter] public string WorldId { get; set; }
        [Inject] private HttpClient Client { get; set; }
        
        

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var world = await Client.GetFromJsonAsync<World>($"api/v1/Worlds/WorldView/{WorldId}");
            if (world != null)
            {
                _world = world;
            }
        }
    }
}