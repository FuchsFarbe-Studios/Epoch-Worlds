using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    public partial class UserWorlds
    {
        /// <summary> The active world. </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        private List<WorldDTO> _userWorlds = new List<WorldDTO>();
        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private NavigationManager Nav { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var worlds = await Client.GetFromJsonAsync<List<WorldDTO>>($"api/v1/Worlds/User?ownerId={Auth.CurrentUser.UserID}");
            if (worlds.Any())
                _userWorlds.AddRange(worlds);
        }
    }
}