using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.User
{
    /// <inheritdoc />
    [Authorize]
    public partial class Worlds
    {
        private bool _loading;
        private IEnumerable<World> _userWorlds = new List<World>();

        /// <inheritdoc />
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await GetUserWorlds();
        }

        private async Task GetUserWorlds()
        {
            _loading = true;
            try
            {
                var worlds = await Client.GetFromJsonAsync<IEnumerable<World>>($"api/v1/Worlds/User/{Auth.CurrentUser.UserID}");
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