// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary> Redirect to login. </summary>
    public partial class RedirectToLogin : ComponentBase
    {
        /// <summary>
        ///     Gets or sets the login URL.
        /// </summary>
        [Parameter] public string LoginUrl { get; set; } = NavRef.Auth.Login;

        [Inject] private NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Nav.NavigateTo(LoginUrl);
        }
    }
}