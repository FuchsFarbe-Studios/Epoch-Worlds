using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages
{
    public partial class Contact
    {
        private ContactDTO _model = new ContactDTO();
        private bool _submitting;
        private bool _success;
        private Dictionary<string, List<string>> _errorDict = new Dictionary<string, List<string>>();
        /// <summary> Injected HttpClient </summary>
        [Inject] public HttpClient Client { get; set; }
        /// <summary>
        ///     Injected EpochAuthProvider
        /// </summary>
        [Inject] public EpochAuthProvider Auth { get; set; }
        /// <summary> Injected ILogger </summary>
        [Inject] public ILogger<Contact> Logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Auth.CurrentUser != null)
            {
                _model.UserName = Auth.CurrentUser.UserName;
                _model.Email = Auth.CurrentUser.Email;
            }
        }

        private async Task HandleContactSubmitAsync(EditContext arg)
        {
            _submitting = true;
            _success = arg.Validate();
            if (!_success)
            {
                _submitting = false;
                return;
            }
            var response = await Client.PostAsJsonAsync("api/v1/Contact", _model);
            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError("Contact form failed to submit");
                _errorDict = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
            }
            else
            {
                Logger.LogInformation("Contact form submitted");
                _model = new ContactDTO
                         {
                             UserName = Auth?.CurrentUser?.UserName ?? null,
                             Email = Auth?.CurrentUser?.Email ?? null,
                             Message = null
                         };
            }
            _submitting = false;
        }
    }
}