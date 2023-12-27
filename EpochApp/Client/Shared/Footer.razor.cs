using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    public partial class Footer
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}