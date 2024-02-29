// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages
{
    /// <summary> About page. </summary>
    public partial class About
    {
        [Inject] private HttpClient Http { get; set; }

        private List<ClientSettingDTO> AboutContents { get; set; } = new List<ClientSettingDTO>();

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var aboutContents = await Http.GetFromJsonAsync<List<ClientSettingDTO>>("api/v1/Settings/About");
            if (aboutContents != null)
                AboutContents = aboutContents;
        }
    }
}