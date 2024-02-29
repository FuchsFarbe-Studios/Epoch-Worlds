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
 #pragma warning disable CS1591// Missing XML comment for publicly visible type or member
        [Parameter] public bool IsMudMenu { get; set; } = false;
 #pragma warning restore CS1591// Missing XML comment for publicly visible type or member
    }
}