using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Client.Shared
{
    /// <summary> Footer component. </summary>
    public partial class Footer
    {
        /// <summary> The child content. </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter(Name = "Settings")] protected SiteSettings Settings { get; set; }
    }
}