using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Text;

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

        protected async Task OutputOptionsAsync(MouseEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Model.LangName}");
            sb.AppendLine($"Owner: {Model.OwnerID}");
            sb.AppendLine("Phonology: ");
            sb.AppendLine($"Consonants: {Model.Phonology.Consonants}");
            sb.AppendLine($"Vowels: {Model.Phonology.Vowels}");
            sb.AppendLine($"Illegal Combinations: {Model.Phonology.IllegalOpts.IllegalCombos}");
            sb.AppendLine($"Vowel Options: {Model.Phonology.VowelOpts.UseVowelProbabilities}");
            sb.AppendLine($"Vowel Options: {Model.Phonology.VowelOpts.VowelAtStart}");
            sb.AppendLine($"Vowel Options: {Model.Phonology.VowelOpts.VowelAtEnd}");
            Logger.LogInformation(sb.ToString());
        }

        private async Task CancelOptionsAsync(MouseEventArgs e)
        {
            Nav.NavigateTo("/Manual/Language");
            await Task.CompletedTask;
        }

        private async Task SaveOptionsAsync(EditContext context = null)
        {
            Model.OwnerID = Auth.CurrentUser.UserID;
            var response = await Client.PostAsJsonAsync<LangOptions>("api/v1/Options/Language", Model);
            var lang = await response.Content.ReadFromJsonAsync<LangOptions>();
            if (response.IsSuccessStatusCode)
                Nav.NavigateTo($"/Manual/Language/E/{lang.OptionsID}");
        }

        private async Task SubmitOptionsAsync(EditContext context = null)
        {
            await SaveOptionsAsync();
        }
    }
}