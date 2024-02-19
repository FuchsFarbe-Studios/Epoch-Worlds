using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    public partial class SubNav
    {
        /// <summary>
        ///     The currently active world.
        /// </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
    }
}