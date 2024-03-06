// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages
{
    /// <summary> About page. </summary>
    public partial class About
    {
        private List<ClientSetting> _aboutContents = new List<ClientSetting>();

        /// <summary>
        /// Site settings.
        /// </summary>
        [CascadingParameter(Name = "Settings")] protected SiteSettings SiteSettings { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var abouts = SiteSettings?.AboutSettings;
        }
    }
}