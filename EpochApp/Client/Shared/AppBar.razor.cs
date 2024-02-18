using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EpochApp.Client.Shared
{
    /// <summary> AppBar component </summary>
    public partial class AppBar
    {
        private bool _showMenu = false;
        [Inject] private EpochAuthProvider Auth { get; set; }

        private async Task ShowMenuAsync(MouseEventArgs mouseEventArgs)
        {
            _showMenu = !_showMenu;
            await Task.CompletedTask;
        }
    }
}