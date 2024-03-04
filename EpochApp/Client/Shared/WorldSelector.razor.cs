using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary> A selector for worlds. </summary>
    public partial class WorldSelector
    {
        private bool _condensed;
        private bool _isDirty = true;
        private WorldDTO _selectedWorld;
        private List<WorldDTO> _userWorlds = new List<WorldDTO>();

        /// <summary>
        ///     The event that is called when the selected world is changed.
        /// </summary>
        [Parameter] public EventCallback<WorldDTO> OnNewWorldChanged { get; set; }

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

        [Inject] private IWorldService Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
            {
                var newWorlds = await Client.GetUserWorldsAsync(Auth.CurrentUser.UserID);
                if (newWorlds.Count != 0)
                    _userWorlds.AddRange(newWorlds);
            }
            _selectedWorld = _userWorlds.FirstOrDefault(x => x?.IsActiveWorld == true) ?? _userWorlds.FirstOrDefault();
            await WorldChanged(_selectedWorld);
            await base.OnInitializedAsync();
        }

        private async Task WorldChanged(WorldDTO e)
        {
            var oldResponse = await Client.UpdateActiveUserWorldsAsync(e);
            _selectedWorld = _userWorlds.FirstOrDefault(x => x.WorldId == oldResponse.WorldId);
            await OnNewWorldChanged.InvokeAsync(_selectedWorld);
        }
    }
}