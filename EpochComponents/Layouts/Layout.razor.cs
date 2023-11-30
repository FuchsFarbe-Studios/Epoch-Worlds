// EpochWorlds
// EpochDictionary.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using Microsoft.AspNetCore.Components;

namespace EpochComponents.Layouts
{
    public partial class Layout
    {
        [Parameter] public RenderFragment HeaderContent { get; set; }
        [Parameter] public RenderFragment LayoutContent { get; set; }
        [Parameter] public RenderFragment FooterContent { get; set; }
    }
}