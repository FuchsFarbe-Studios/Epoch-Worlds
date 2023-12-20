// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Client.Services;
using EpochApp.Kit.Forms;
using EpochApp.Shared.DataTransfer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Auth
{
    public partial class Login
    {

        [Inject] public HttpClient Client { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public EpochAuthProvider _auth { get; set; }
        private LoginDTO _login = new LoginDTO();
        private ServerSideValidator _validator;
        private bool _loggingIn = false;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public async Task AttemptLoginAsync(EditContext ctx)
        {
            _loggingIn = true;
            _validator.ClearErrors();
            _errors.Clear();
            if (!ctx.Validate())
            {
                _loggingIn = false;
                var errors = new Dictionary<String, List<String>>()
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
                var result = await Client.PostAsJsonAsync<LoginDTO>("api/v1/EpochUsers/Authenticate", _login);
                await Task.Delay(500);
                if (result.IsSuccessStatusCode)
                {
                    var token = await result.Content.ReadAsStringAsync();
                    //await _auth.LoginAsync(_login.UserName, _login.Password);
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