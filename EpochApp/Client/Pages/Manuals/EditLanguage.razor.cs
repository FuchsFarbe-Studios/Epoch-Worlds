using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Manuals
{
    /// <summary>
    ///     The edit language page.
    /// </summary>
    public partial class EditLanguage
    {
        /// <summary> The language id. </summary>
        [Parameter] public string LanguageId { get; set; }

        /// <inheritdoc />
        protected override LangOptions Model { get; set; } = new LangOptions
                                                             {
                                                                 Phonology = new PhonologyOptions
                                                                             {
                                                                                 IllegalOpts = new IllegalComboOptions(),
                                                                                 VowelOpts = new VowelOptions()
                                                                             }
                                                             };

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetLangOptions();
        }

        private async Task GetLangOptions()
        {
            var langOps = await Client.GetFromJsonAsync<LangOptions>($"api/v1/Options/Language/{Auth.CurrentUser.UserID}/{LanguageId}");
            if (langOps is null)
            {
                Model = new LangOptions
                        {
                            Phonology = new PhonologyOptions
                                        {
                                            IllegalOpts = new IllegalComboOptions(),
                                            VowelOpts = new VowelOptions()
                                        }
                        };
            }
            else
            {
                Model = langOps;
            }
        }

        private async Task UpdateOptionsAsync(EditContext ctx)
        {
            if (ctx.Validate())
            {
                var response = await Client.PutAsJsonAsync($"api/v1/Options/Language/{Auth.CurrentUser.UserID}/{LanguageId}", Model);
                if (response.IsSuccessStatusCode)
                {
                    Logger.LogInformation("Successfully updated language options");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Logger.LogError("Failed to update language options");
                    Logger.LogError(error);
                }
            }
        }
    }
}