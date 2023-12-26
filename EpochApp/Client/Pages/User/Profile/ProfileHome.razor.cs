// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.User
{
    [Authorize]
    public partial class ProfileHome
    {
        [Inject] public EpochAuthProvider Auth { get; set; }
    }
}