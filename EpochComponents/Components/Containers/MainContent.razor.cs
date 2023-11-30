// EpochWorlds
// EpochDictionary.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Enums;
using Microsoft.AspNetCore.Components;

namespace EpochComponents.Components.Containers
{
    public partial class MainContent
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public ContainerWidth Width { get; set; } = ContainerWidth.Lg;
    }
}