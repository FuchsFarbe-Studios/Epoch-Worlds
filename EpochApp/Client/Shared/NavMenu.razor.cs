// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary> Navigation menu. </summary>
    public partial class NavMenu
    {
        /// <summary>
        ///    The state of the menu.
        /// </summary>
        [CascadingParameter(Name = "MenuOpened")] public bool IsMenuOpened { get; set; }
    }
}