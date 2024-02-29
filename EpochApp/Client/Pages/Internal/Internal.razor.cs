using EpochApp.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Internal
{
    /// <summary> Internal page. </summary>
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class Internal
    {
        [Inject] private EpochAuthProvider Auth { get; set; }
    }
}