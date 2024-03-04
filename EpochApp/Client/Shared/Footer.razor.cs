using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;

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