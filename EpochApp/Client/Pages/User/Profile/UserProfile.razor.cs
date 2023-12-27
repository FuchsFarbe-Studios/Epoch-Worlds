// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared.Users;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.User
{
    /// <summary> The profile home page. </summary>
    [Authorize]
    public partial class UserProfile
    {
        private bool _loading = true;
        private Profile _profile;

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            await Task.Delay(1000);
            try
            {
                var prof = await Client.GetFromJsonAsync<Profile>($"api/v1/Profiles/{Auth.CurrentUser.UserID}");
                if (prof != null)
                    _profile = prof;
                _loading = false;
                await Task.CompletedTask;
            }
            catch (Exception exception)
            {
                _loading = false;
                Console.WriteLine(exception);
                throw;
            }

            _loading = false;
        }
    }
}