using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Site.Community
{
    public partial class Blog
    {
        /// <summary>
        ///     The blog id to display.
        /// </summary>
        [Parameter] public int? BlogId { get; set; }
    }
}