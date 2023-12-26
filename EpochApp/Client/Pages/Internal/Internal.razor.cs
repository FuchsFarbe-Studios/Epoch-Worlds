using EpochApp.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Internal
{
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class Internal
    {
        [Inject] public EpochAuthProvider Auth { get; set; }
    }
}