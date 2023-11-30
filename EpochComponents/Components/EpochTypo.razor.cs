// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Enums;
using Microsoft.AspNetCore.Components;

namespace EpochComponents.Components
{
    public partial class EpochTypo
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EpochTypoType Typo { get; set; } = EpochTypoType.Body1;
        [Parameter] public ColorStyle Color { get; set; } = ColorStyle.LightPink;
    }
}