using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary>
    /// Redirects to the dashboard page.
    /// </summary>
    public partial class RedirectToDashboard
    {
        [Inject] private NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Nav.NavigateTo(NavRef.UserNav.Dashboard);
        }
    }
}