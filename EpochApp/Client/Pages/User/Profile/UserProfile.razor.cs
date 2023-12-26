using EpochApp.Shared;
using EpochApp.Shared.Users;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.User
{
    public partial class UserProfile
    {
        private bool _isEdit = false;
        [Parameter] public UserData UserData { get; set; }
        [Parameter] public Profile Profile { get; set; }
        [Inject] public HttpClient Client { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await GetProfileAsync();
        }
        private async Task GetProfileAsync()
        {
            var profile = await Client.GetFromJsonAsync<Profile>($"api/v1/Profiles/{UserData.UserID}");
            if (profile != null)
                Profile = profile;
            else
                Profile = new Profile();

        }
    }
}