using EpochApp.Client.Services;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.User
{
    [Authorize]
    public partial class Worlds
    {
        private bool _loading;
        private IEnumerable<World> _userWorlds = new List<World>();

        [Inject] public ILogger<Worlds> Logger { get; set; }
        [Inject] public EpochAuthProvider Auth { get; set; }
        [Inject] public HttpClient Client { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await GetUserWorlds();
        }

        private async Task GetUserWorlds()
        {
            _loading = true;
            try
            {
                var worlds = await Client.GetFromJsonAsync<IEnumerable<World>>($"api/Worlds/User/{Auth.CurrentUser.UserID}");
                if (worlds != null)
                    _userWorlds = worlds;
                _loading = false;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                _userWorlds = new List<World>();
                _loading = false;
            }
        }
    }
}