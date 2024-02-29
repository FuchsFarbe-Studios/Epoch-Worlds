using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     Sub navigation for logged-in users.
    /// </summary>
    public partial class SubNav
    {
        /// <summary>
        ///     The currently active world.
        /// </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
    }
}