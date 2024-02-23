using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.ViewContent
{
    /// <summary>
    ///     A page to display a public profile.
    /// </summary>
    public partial class ProfileView
    {
        private UserProfileDTO _profile = null!;

        /// <summary>
        ///     The user id of the profile to display.
        /// </summary>
        [Parameter] public string UserId { get; set; }

        [Inject] private HttpClient Client { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!string.IsNullOrWhiteSpace(UserId) && Guid.TryParse(UserId, out var userId))
            {
                var profile = await Client.GetFromJsonAsync<UserProfileDTO>($"api/v1/Profiles/PublicProfile?userId={userId}");
                if (profile != null)
                    _profile = profile;
            }
            // _profile = await Client.GetFromJsonAsync<UserProfileDTO>($"");
        }
    }
}