using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Auth
{
    /// <summary> Reset password page. </summary>
    public partial class ResetPassword
    {
        //private EpochValidator _validator;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private bool _submitting = false;
        /// <summary>
        ///     The token to verify password reset.
        /// </summary>
        [Parameter] public string Token { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Token != null)
            {
                Model = new ResetPasswordDTO { ResetToken = Token };
            }
            else
            {
                Nav.NavigateTo("/");
            }
            await base.OnInitializedAsync();
        }

        private async Task ResetPasswordAsync(EditContext arg)
        {
            var resetDto = arg.Model as ResetPasswordDTO;
            var response = await Client.PostAsJsonAsync<ResetPasswordDTO>("api/v1/EpochUsers/Reset-Password", resetDto);
            if (response.IsSuccessStatusCode)
            {
                Nav.NavigateTo(NavRef.Auth.Login);
            }
            else
            {
                _errors = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
            }
        }
    }
}