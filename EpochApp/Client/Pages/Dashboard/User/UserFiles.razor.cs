using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.User
{
    /// <summary>
    ///    The user files component.
    /// </summary>
    public partial class UserFiles
    {
        private List<UserFileDTO> _files { get; set; } = new List<UserFileDTO>();

        [CascadingParameter] private WorldDTO ActiveWorld { get; set; }

        [Inject] private IWorldService WorldService { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Auth != null && Auth.CurrentUser != null)
                _files = await Client.GetFromJsonAsync<List<UserFileDTO>>($"api/v1/UserFiles/UserFiles/{Auth.CurrentUser.UserID}");
        }

        private async Task OnUserTabSelected(MouseEventArgs arg)
        {
            _files = await Client.GetFromJsonAsync<List<UserFileDTO>>($"api/v1/UserFiles/UserFiles/{Auth.CurrentUser.UserID}");
        }

        private async Task OnWorldTabSelected(MouseEventArgs arg)
        {
            _files = await Client.GetFromJsonAsync<List<UserFileDTO>>($"api/v1/UserFiles/WorldFiles/{Auth.CurrentUser.UserID}/{ActiveWorld.WorldId}");
        }
    }
}