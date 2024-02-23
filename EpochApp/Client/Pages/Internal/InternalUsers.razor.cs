using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Internal
{
    /// <summary>
    ///     A page to display users.
    /// </summary>
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class InternalUsers
    {
        private List<UserData> _users = new List<UserData>();
        [Inject] private HttpClient Client { get; set; }
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var users = await Client.GetFromJsonAsync<List<UserData>>("api/v1/EpochUsers");
            if (users != null)
                _users = users;
        }
    }
}