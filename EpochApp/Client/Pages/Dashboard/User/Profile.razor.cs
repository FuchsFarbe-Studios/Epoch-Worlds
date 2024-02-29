using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.User
{
    /// <summary>
    ///     Profile page for a logged in user.
    /// </summary>
    [Authorize]
    public partial class Profile
    {
        private ProfileDTO _profileData = null!;
        private bool _showProfileEdit;
        private List<SocialMedia> _socials = new List<SocialMedia>();
        private bool _updatingProfile;
        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private ILogger<Profile> Logger { get; set; }
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var userId = Auth.CurrentUser.UserID;
            var profileData = await Client.GetFromJsonAsync<ProfileDTO>($"api/v1/Profiles/{userId}");
            _profileData = profileData;
        }

        private async Task ToggleEditAsync()
        {
            _showProfileEdit = !_showProfileEdit;
            var socials = await Client.GetFromJsonAsync<List<SocialMedia>>("api/v1/Lookups/lkSocials");
            if (socials.Any())
                _socials = socials;
            await Task.CompletedTask;
        }

        private async Task RemoveSocialAsync(SocialDTO social)
        {
            if (_profileData.Socials.Contains(social))
                _profileData.Socials.Remove(social);
            //StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task AddSocialMediaAsync()
        {
            _profileData.Socials.Add(new SocialDTO
                                     {
                                         Social = _socials.FirstOrDefault(),
                                         Handle = ""
                                     });
            //StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task UpdateProfileAsync(EditContext ctx)
        {
            _updatingProfile = true;
            var profile = ctx.Model as ProfileDTO;
            profile.UserID = Auth.CurrentUser.UserID;

            var response = await Client.PutAsJsonAsync<ProfileDTO>($"api/v1/Profiles/{Auth.CurrentUser.UserID}", profile);
            if (response.IsSuccessStatusCode)
            {
                _showProfileEdit = false;
                _profileData = await Client.GetFromJsonAsync<ProfileDTO>($"api/v1/Profiles/{Auth.CurrentUser.UserID}");
                Logger.LogInformation("Profile Updated!");
                StateHasChanged();
            }
            else
            {
                Logger.LogError("Failed to update profile...");
            }
            _updatingProfile = false;
        }

        private Task AvatarImageChanged(UserFileDTO arg)
        {
            _profileData.AvatarImg = arg.FilePath;
            return Task.CompletedTask;
        }

        private Task CoverImageChanged(UserFileDTO arg)
        {
            _profileData.CoverImg = arg.FilePath;
            return Task.CompletedTask;
        }
    }
}