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
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public EpochAuthProvider _auth { get; set; }
        [Inject] public ILogger<Logout> _logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            _auth.Logout();
            _logger.LogInformation($"User: {_auth.CurrentUser.UserName} logged out.");
            NavigationManager.NavigateTo("/");
            await base.OnInitializedAsync();
        }
    }
}