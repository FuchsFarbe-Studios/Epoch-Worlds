// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    public partial class RedirectToLogin : ComponentBase
    {
        [Parameter] public string LoginUrl { get; set; }
        [Inject] public NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Nav.NavigateTo(LoginUrl);
        }
    }
}