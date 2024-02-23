using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Internal
{
    /// <summary>
    ///     A page to display contact attempts.
    /// </summary>
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class InternalContacts
    {
        /// <summary> Injected HttpClient </summary>
        [Inject] public HttpClient Client { get; set; }
        private List<InternalContactDTO> _contacts { get; set; } = new List<InternalContactDTO>();
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _contacts = await Client.GetFromJsonAsync<List<InternalContactDTO>>("api/v1/Contact/Internal");
        }
    }
}