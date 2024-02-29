// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Auth
{
    /// <summary> Logout page. </summary>
    public partial class Logout
    {
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private ILogger<Logout> Logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            Auth.Logout();
            Logger.LogInformation($"User: {Auth.CurrentUser.UserName} logged out.");
            Nav.NavigateTo("/");
            await base.OnInitializedAsync();
        }
    }
}