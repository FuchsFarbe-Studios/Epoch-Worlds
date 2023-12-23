// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Kit.Forms;
using EpochApp.Kit.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Auth
{
    public partial class Login
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private bool _loggingIn;
        private LoginDTO _loginDto = new LoginDTO();
        private EpochValidator _validator;
        [Inject] public HttpClient Client { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public EpochAuthProvider _auth { get; set; }

        private async Task AttemptLoginAsync(EditContext ctx)
        {
            _loggingIn = true;
            _errors.Clear();
            if (!ctx.Validate())
            {
                _loggingIn = false;
                var errors = new Dictionary<string, List<string>>
                             {
                                 {
                                     "Errors!", ctx.GetValidationMessages().ToList()
                                 }

                             };
                _errors = errors;
                _validator.DisplayErrors(errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.Key);
                    foreach (var message in error.Value)
                        Console.WriteLine(message);
                }
                return;
            }
            if (ctx.Validate())
            {
                var result = await Client.PostAsJsonAsync("api/v1/EpochUsers/Authentication", (LoginDTO)ctx.Model);
                await Task.Delay(500);
                if (result.IsSuccessStatusCode)
                {
                    var token = await result.Content.ReadAsStringAsync();
                    await _auth.LoginAsync(_loginDto.UserName, _loginDto.Password);
                    _loggingIn = false;
                    if (_auth.CurrentUser != null)
                        NavigationManager.NavigateTo("/");
                }
                else
                {
                    _loggingIn = false;
                    var errors = await result.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
                    _errors = errors;
                    _validator.DisplayErrors(errors);
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.Key);
                        foreach (var message in error.Value)
                            Console.WriteLine(message);
                    }
                }
            }
            _loggingIn = false;
        }
    }
}