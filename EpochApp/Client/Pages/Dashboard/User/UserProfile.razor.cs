using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

#pragma warning disable CS0414// Field is assigned but its value is never used

namespace EpochApp.Client.Pages.User
{
    /// <summary>
    ///     Profile page for a logged in user.
    /// </summary>
    [Authorize]
    public partial class UserProfile
    {
        private bool _loading = true;
        private ProfileDTO _userProfile = null!;

        [Inject] private IProfileSerivce ProfileService { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            _userProfile = await ProfileService.GetProfileByUserIdAsync(Auth.CurrentUser.UserID);
            _loading = false;
            await base.OnInitializedAsync();
        }
    }
}