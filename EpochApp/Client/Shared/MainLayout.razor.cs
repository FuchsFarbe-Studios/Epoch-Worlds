using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <inheritdoc />
    public partial class MainLayout
    {
        private bool _drawerOpen;

        private WorldDTO _activeWorld;
        // private bool _isDarkMode;

        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task HandleWorldChanged(WorldDTO arg)
        {
            var activeWorld = await Client.GetFromJsonAsync<WorldDTO>($"api/v1/Worlds/ActiveWorld?ownderId={Auth.CurrentUser.UserID}");
            _activeWorld = activeWorld;
        }
    }
}