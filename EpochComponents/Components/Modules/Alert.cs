// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023
using EpochComponents.Enums;
using EpochComponents.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace EpochComponents.Components.Modules
{
    public class Alert : EpochBase
    {
        [Parameter] public String? Title { get; set; }
        [Parameter] public String AlertMessage { get; set; }
        [Parameter] public EpochState AlertState { get; set; } = EpochState.Info;

        /// <inheritdoc />
        protected override void OnParametersSet()
        {
            Class = $"alert {AlertState.ToDescriptionString()} has-icon";
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            // Small div render tree
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", Class);
            builder.AddAttribute(2, "style", Styles);
            builder.AddAttribute(3, "role", "alert");
            builder.OpenElement(5, "div");
            builder.AddAttribute(6, "class", "close-button floating");
            builder.AddAttribute(7, "aria-label", "Close");
            builder.CloseElement();
            builder.AddContent(4, ChildContent);
            builder.CloseElement();
        }
    }

}