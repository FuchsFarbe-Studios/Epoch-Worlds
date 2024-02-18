using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <inheritdoc />
    public partial class MainLayout
    {

        private WorldDTO _activeWorld;

        private bool _drawerOpen;
        // private bool _isDarkMode;

        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private ILogger<MainLayout> Logger { get; set; }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private async Task HandleWorldChanged(WorldDTO arg)
        {
            Logger.LogInformation("World Changed: {WorldID}", arg.WorldID);
            var activeWorld = await Client.GetFromJsonAsync<WorldDTO>($"api/v1/Worlds/ActiveWorld?ownerId={Auth.CurrentUser.UserID}");
            if (activeWorld.WorldID == arg.WorldID)
                _activeWorld = arg;
            await Task.CompletedTask;
        }
    }
}