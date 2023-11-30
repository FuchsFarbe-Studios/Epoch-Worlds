// EpochWorlds
// EpochBase.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 30-11-2023
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace EpochComponents.Components;

public abstract class EpochBase : ComponentBase
{
    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Styles { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);

        // Small div render tree
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", Class);
        builder.AddAttribute(2, "style", Styles);
        builder.AddContent(3, ChildContent);
        builder.CloseElement();
        builder.CloseComponent();
    }
}