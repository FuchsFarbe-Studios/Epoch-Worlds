// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary> Redirect to logout. </summary>
    public partial class RedirectToLogout : ComponentBase
    {
        /// <summary>
        ///     Gets or sets the logout URL.
        /// </summary>
        [Parameter] public string LogoutUrl { get; set; } = NavRef.Auth.Logout;

        [Inject] private NavigationManager NavigationManager { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            NavigationManager.NavigateTo(LogoutUrl);
        }
    }
}