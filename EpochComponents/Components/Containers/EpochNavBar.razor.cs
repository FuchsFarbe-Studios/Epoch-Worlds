// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Enums;
using Microsoft.AspNetCore.Components;

namespace EpochComponents.Components.Containers
{
    public partial class EpochNavBar
    {
        [Parameter] public RenderFragment NavLeft { get; set; }
        [Parameter] public RenderFragment NavRight { get; set; }
        [Parameter] public ColorStyle Color { get; set; } = ColorStyle.Dark;
        [Parameter] public Size Size { get; set; } = Size.Md;
        [Parameter] public Boolean UseBranding { get; set; } = true;
        [Parameter] public String BrandName { get; set; } = "Flat UI";
        [Parameter] public String BrandHref { get; set; } = "/";
        [Parameter] public String BrandImageSrc { get; set; } = "";
        [Parameter] public String BrandImageAlt { get; set; } = "";
    }
}