using EpochApp.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.User
{
    /// <summary>
    ///     Profile page for a logged in user.
    /// </summary>
    [Authorize]
    public partial class Profile
    {
        [Inject] private EpochAuthProvider Auth { get; set; }
    }
}