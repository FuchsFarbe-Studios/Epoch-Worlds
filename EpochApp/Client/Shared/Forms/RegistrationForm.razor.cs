using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Client.Shared
{
    public partial class RegistrationForm
    {
        private RegistrationDTO _registration = new RegistrationDTO();
        private EpochValidator _validator;
        [Inject] public ILogger<RegistrationForm> Logger { get; set; }
        [Inject] public NavigationManager Nav { get; set; }
        [Inject] public EpochAuthProvider Auth { get; set; }
        [Inject] public HttpClient Client { get; set; }

        private async Task AttemptRegistrationAsync(EditContext ctx)
        {
            var result = await Client.PostAsJsonAsync("api/v1/EpochUsers/Registration", _registration);
            if (result.IsSuccessStatusCode)
            {
                await Auth.LoginAsync(_registration.UserName, _registration.Password);
                Nav.NavigateTo("/");
            }
            else
            {
                var errors = await result.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
                foreach (var error in errors)
                {
                    Logger.LogError($"Error registering user: {error.Key}\n\t", error.Value, "\n");
                }
            }
        }
    }
}