using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Manuals
{
    /// <summary>
    ///     The create language page.
    /// </summary>
    public partial class CreateLanguage
    {
        /// <inheritdoc />
        protected override LangOptions Model { get; set; } = new LangOptions
                                                             {
                                                                 Phonology = new PhonologyOptions
                                                                             {
                                                                                 IllegalOpts = new IllegalComboOptions(),
                                                                                 VowelOpts = new VowelOptions()
                                                                             }
                                                             };
        private async Task SaveOptionsAsync(EditContext context = null)
        {
            Model.OwnerID = Auth.CurrentUser.UserID;
            var response = await Client.PostAsJsonAsync<LangOptions>("api/v1/Options/Language", Model);
            var lang = await response.Content.ReadFromJsonAsync<LangOptions>();
            if (response.IsSuccessStatusCode)
            {
                Nav.NavigateTo($"/Manual/Language/E/{lang.OptionsID}");
            }
        }

        private async Task SubmitOptionsAsync(EditContext context = null)
        {
            await SaveOptionsAsync();
        }
    }
}