// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Kit.Services;
using EpochApp.Shared.DataTransfer;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Auth
{
    public partial class Registration
    {

        private RegistrationDTO _registration = new RegistrationDTO();
        [Inject] public HttpClient Http { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ILogger<Registration> Logger { get; set; }
        [Inject] public EpochAuthProvider Auth { get; set; }

        private async Task AttemptRegistrationAsync()
        {
            Logger.LogInformation("Attempting to register user: \n\t",
                                  _registration.UserName, "\n\t\t",
                                  _registration.Password, "\n\t\t",
                                  _registration.DateOfBirth, "\n\t\t"
                                  , _registration.Email, "\n\t\t");
            var result = await Http.PostAsJsonAsync("api/v1/EpochUsers/Registration", _registration);
            if (result.IsSuccessStatusCode)
            {
                await Auth.LoginAsync(_registration.UserName, _registration.Password);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Dictionary<string, List<string>> errors = await result.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
                foreach (var error in errors)
                {
                    Logger.LogError($"Error registering user: {error.Key}\n\t", error.Value, "\n");
                }
            }
        }
    }
}