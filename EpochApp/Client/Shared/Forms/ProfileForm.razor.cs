using EpochApp.Shared.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared.Forms
{
    public partial class ProfileForm
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private bool _isLoading = false;
        private EpochValidator _validator;
        [Inject] public ILogger<ProfileForm> Logger { get; set; }
        [Inject] public HttpClient Client { get; set; }
        /// <summary> Profile to edit. </summary>
        [Parameter] public Profile Profile { get; set; } = new Profile();
        private async Task UpdateProfileAsync(EditContext ctx)
        {
            _isLoading = true;
            if (!ctx.Validate())
            {
                var messages = ctx.GetValidationMessages();
                _errors = new Dictionary<string, List<string>>
                          {
                              { "Error!", messages.ToList() }
                          };
                _validator.DisplayErrors(_errors);
                _isLoading = false;
                return;
            }
            _validator.ClearErrors();
            var response = await Client.PutAsJsonAsync<Profile>($"api/v1/Profiles/{Profile.UserID}", Profile);

            if (!response.IsSuccessStatusCode)
            {
                _errors = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
                _validator.DisplayErrors(_errors);
            }
            _isLoading = false;
        }
    }
}