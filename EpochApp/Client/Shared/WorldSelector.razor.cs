using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <summary> A selector for worlds. </summary>
    public partial class WorldSelector
    {
        private bool _condensed;
        private bool _isDirty = true;
        private UserWorldDTO _newSelectedWorld;
        private List<UserWorldDTO> _newUserWorlds = new List<UserWorldDTO>();
        private WorldDTO _selectedWorld;
        private List<WorldDTO> _userWorlds = new List<WorldDTO>();

        /// <summary>
        ///     The event that is called when the selected world is changed.
        /// </summary>
        [Parameter] public EventCallback<WorldDTO> OnWorldChanged { get; set; }

        [Parameter] public EventCallback<UserWorldDTO> OnNewWorldChanged { get; set; }

        /// <summary>
        ///     The event that is called when the selected world is changed.
        /// </summary>
        [CascadingParameter(Name = "IsNavCollapsed")] public bool Condensed
        {
            get => _condensed;
            set
            {
                if (_condensed == value)
                    _isDirty = false;
                else
                    _isDirty = true;
                _condensed = value;
            }
        }

        [Inject] private HttpClient Client { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
            {
                var worlds = await Client.GetFromJsonAsync<List<WorldDTO>>($"api/v1/Worlds/User?ownerId={Auth.CurrentUser.UserID}");
                var newWorlds = await Client.GetFromJsonAsync<List<UserWorldDTO>>($"api/v2/Worlds/UserWorlds?userId={Auth.CurrentUser.UserID}");
                if (worlds.Count != 0)
                    _userWorlds.AddRange(worlds);
                if (newWorlds.Count != 0)
                    _newUserWorlds.AddRange(newWorlds);
            }
            _selectedWorld = _userWorlds.FirstOrDefault(x => x?.IsActiveWorld == true) ?? _userWorlds.FirstOrDefault();
            await WorldChanged(_selectedWorld);
            await base.OnInitializedAsync();
        }

        /// <inheritdoc />
        protected override bool ShouldRender()
        {
            return _isDirty;
        }

        private async Task WorldChanged(WorldDTO e)
        {
            var oldResponse = await Client.PutAsJsonAsync<WorldDTO>("api/v1/Worlds/ActiveWorld", e);
            // var newResponse = await Client.PutAsJsonAsync<NewWorldDTO>();
            if (oldResponse.IsSuccessStatusCode)
            {
                var updatedWorld = await oldResponse.Content.ReadFromJsonAsync<WorldDTO>();
                _selectedWorld = _userWorlds.FirstOrDefault(x => x.WorldID == updatedWorld.WorldID);
            }
            _newSelectedWorld = _newUserWorlds.FirstOrDefault(x => x.WorldId == _selectedWorld.WorldID);
            await OnNewWorldChanged.InvokeAsync(_newSelectedWorld);
            await OnWorldChanged.InvokeAsync(_selectedWorld);
        }
    }
}