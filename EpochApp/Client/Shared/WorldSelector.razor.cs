using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    public partial class WorldSelector
    {
        private WorldDTO _selectedWorld;
        private List<WorldDTO> _userWorlds = new List<WorldDTO>();
        /// <summary>
        ///     The event that is called when the selected world is changed.
        /// </summary>
        [Parameter] public EventCallback<WorldDTO> OnWorldChanged { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }


        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
            {
                var worlds = await Client.GetFromJsonAsync<List<WorldDTO>>($"api/v1/Worlds/User?ownerId={Auth.CurrentUser.UserID}");
                if (worlds.Count != 0)
                    _userWorlds.AddRange(worlds);
            }
            _selectedWorld = _userWorlds.FirstOrDefault(x => x?.IsActiveWorld == true) ?? _userWorlds.FirstOrDefault();
            await WorldChanged(_selectedWorld);
        }

        private async Task WorldChanged(WorldDTO e)
        {
            var response = await Client.PutAsJsonAsync<WorldDTO>("api/v1/Worlds/ActiveWorld", e);
            if (response.IsSuccessStatusCode)
            {
                var updatedWorld = await response.Content.ReadFromJsonAsync<WorldDTO>();
                _selectedWorld = _userWorlds.FirstOrDefault(x => x.WorldID == updatedWorld.WorldID);
            }
            await OnWorldChanged.InvokeAsync(_selectedWorld);
        }
    }
}