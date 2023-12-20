// EpochWorlds
// DescriptionAttribute.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace EpochApp.Kit.Forms
{
    public partial class EpochField
    {
        [Parameter] public Expression<Func<String>> For { get; set; }
        [Parameter] public String Label { get; set; }
        [Parameter] public Boolean Inline { get; set; } = true;
        [Parameter] public Boolean Floating { get; set; }
        [Parameter] public Boolean Required { get; set; }
        [Parameter] public String Placeholder { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; } = null!;


        protected override void OnParametersSet()
        {
            var attributes = new Dictionary<String, Object>();
            if (!string.IsNullOrEmpty(Placeholder))
                attributes.TryAdd("placeholder", Placeholder);
            if (Required)
                attributes.TryAdd("required", Required);
            attributes.TryAdd("type", "text");
            AdditionalAttributes = attributes;
            base.OnParametersSet();
        }
        /// <inheritdoc />
        protected override Boolean TryParseValueFromString(String value, out String result, out String validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}