using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Auth
{

    /// <summary>
    ///     The verification page for verifying the user's email.
    /// </summary>
    public partial class Verification
    {
        private VerificationDTO _verificationDTO = new VerificationDTO();

        /// <summary>
        ///     The token to verify the user's email.
        /// </summary>
        [Parameter] public string? Token { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] private ILogger<Verification> Logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Token is not null)
            {
                _verificationDTO.Token = Token;
                await Task.Delay(1000);
                await VerifyEmail();
            }
        }

        private async Task VerifyEmail()
        {
            var response = await Client.PostAsJsonAsync<VerificationDTO>("api/v1/EpochUsers/Verification", _verificationDTO);
            if (response.IsSuccessStatusCode)
            {
                Nav.NavigateTo(NavRef.UserNav.Dashboard);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Logger.LogError(content);
            }
        }
    }
}