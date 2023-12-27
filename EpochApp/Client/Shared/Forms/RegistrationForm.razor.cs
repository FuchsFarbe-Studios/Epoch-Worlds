using EpochApp.Shared;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Client.Shared
{
    public partial class RegistrationForm
    {
        private bool _loading = false;
        private RegistrationDTO _registration = new RegistrationDTO();
        private EpochValidator _validator;

        private async Task AttemptRegistrationAsync(EditContext ctx)
        {
            _loading = true;
            _validator.ClearErrors();
            if (ctx.Validate())
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
                    _validator.DisplayErrors(errors);
                    foreach (var error in errors)
                    {
                        Logger.LogError($"Error registering user: {error.Key}\n\t", error.Value, "\n");
                    }
                }
            }
            else
            {
                var messages = new List<string>();
                messages.AddRange(ctx.GetValidationMessages());
                _validator.DisplayErrors(messages);
            }

            _loading = false;
        }
    }
}