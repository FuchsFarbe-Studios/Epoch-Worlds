using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EpochApp.Client.Shared
{
    /// <summary> AppBar component </summary>
    public partial class AppBar
    {
        private bool _isDarkMode = false;
        private bool _showMenu = false;

        /// <summary>
        ///    Event callback for toggling dark mode.
        /// </summary>
        [Parameter] public EventCallback<bool> OnDarkModeToggle { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        public bool ShowMenu
        {
            get => _showMenu;
            set
            {
                _showMenu = value;

            }
        }

        private async Task ShowMenuAsync(MouseEventArgs mouseEventArgs)
        {
            ShowMenu = !ShowMenu;
            await Task.CompletedTask;
        }

        private async Task ToggleDarkModeAsync()
        {
            _isDarkMode = !_isDarkMode;
            await OnDarkModeToggle.InvokeAsync(_isDarkMode);
        }
    }
}