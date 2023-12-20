// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using Microsoft.AspNetCore.Components;

namespace EpochApp.Kit.Components.Auth
{
    public partial class RedirectToLogout : ComponentBase
    {
        [Parameter] public String LogoutUrl { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            NavigationManager.NavigateTo(LogoutUrl);
        }
    }
}