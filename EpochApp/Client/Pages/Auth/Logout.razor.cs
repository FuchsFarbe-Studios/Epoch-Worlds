// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Auth
{
    public partial class Logout
    {
        [Inject] public NavigationManager Nav { get; set; }
        [Inject] public EpochAuthProvider Auth { get; set; }
        [Inject] public ILogger<Logout> Logger { get; set; }

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