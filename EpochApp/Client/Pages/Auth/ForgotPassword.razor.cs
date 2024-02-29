using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Auth
{
    /// <summary>
    ///     The forgot password page.
    /// </summary>
    public partial class ForgotPassword
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        private async Task SubmitResetPasswordAsync(EditContext arg)
        {
            var passwordDto = arg.Model as ForgotPasswordDTO;
            var response = await Client.PostAsJsonAsync("api/v1/EpochUsers/Forgot-Password", passwordDto);
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