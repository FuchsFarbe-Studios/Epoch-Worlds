using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     The login form component.
    /// </summary>
    public partial class LoginForm
    {
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private bool _loggingIn = false;
        private LoginDTO _loginDto = new LoginDTO();
        private EpochValidator _validator;

        private async Task AttemptLoginAsync(EditContext ctx)
        {
            Logger.LogInformation($"Logging in: {_loginDto.UserName}");
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
                var result = await Client.PostAsJsonAsync<LoginDTO>("api/v1/EpochUsers/Authentication", (LoginDTO)ctx.Model);
                await Task.Delay(500);
                if (result.IsSuccessStatusCode)
                {
                    // ReSharper disable once UnusedVariable
                    var token = await result.Content.ReadAsStringAsync();
                    await Auth.LoginAsync(_loginDto.UserName, _loginDto.Password);
                    _loggingIn = false;
                    if (Auth.CurrentUser != null)
                        Nav.NavigateTo(NavRef.UserNav.Dashboard);
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