using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Internal.Configuration
{
    /// <summary>
    ///     Component for configuring phonemes.
    /// </summary>
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class ConfigurePhonemes
    {
        private Consonant _consonantModel = new Consonant
                                            {
                                                Manner = ConsonantManner.Nasal,
                                                Place = ConsonantPlace.Bilabial,
                                                IsVoiced = false
                                            };

        private List<Consonant> _consonants = new List<Consonant>();

        private Vowel _vowelModel = new Vowel
                                    {
                                        Depth = VowelDepth.Close,
                                        Verticality = VowelVerticality.Front,
                                        IsRounded = false
                                    };

        private List<Vowel> _vowels = new List<Vowel>();
        [Inject] private HttpClient Client { get; set; }
        [Inject] private ILogger<ConfigurePhonemes> Logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _consonants = await RefreshConsonants();
            _vowels = await RefreshVowels();
        }

        private async Task<List<Vowel>> RefreshVowels()
        {
            _vowels = await Client.GetFromJsonAsync<List<Vowel>>("api/v1/Builder/Vowels");
            return _vowels;
        }

        private async Task<List<Consonant>> RefreshConsonants()
        {
            _consonants = await Client.GetFromJsonAsync<List<Consonant>>("api/v1/Builder/Consonants");
            return _consonants;
        }

        private async Task AddConsonantAsync(EditContext ctx)
        {
            var consonant = (Consonant)ctx.Model;
            var response = await Client.PostAsJsonAsync("api/v1/Builder/Consonant", consonant);
            if (response.IsSuccessStatusCode)
                Logger.LogInformation("Consonant added successfully");
            else
                Logger.LogWarning("Failed to add consonant");
            _consonantModel = new Consonant
                              {
                                  Manner = ConsonantManner.Nasal,
                                  Place = ConsonantPlace.Bilabial,
                                  IsVoiced = false
                              };
            _consonants = await RefreshConsonants();
            StateHasChanged();
        }

        private async Task AddVowelAsync(EditContext ctx)
        {
            var vowel = (Vowel)ctx.Model;
            var response = await Client.PostAsJsonAsync("api/v1/Builder/Vowel", vowel);
            if (response.IsSuccessStatusCode)
                Logger.LogInformation("Vowel added successfully");
            else
                Logger.LogWarning("Failed to add vowel");
            _vowelModel = new Vowel
                          {
                              Depth = VowelDepth.Close,
                              Verticality = VowelVerticality.Front,
                              IsRounded = false
                          };
            _vowels = await RefreshVowels();
            StateHasChanged();
        }

        private async Task CommitConsonantChangesAsync(Consonant arg)
        {
            var response = await Client.PutAsJsonAsync("api/v1/Builder/Consonant", arg);
            if (response.IsSuccessStatusCode)
                Logger.LogInformation("Consonant updated successfully");
            else
                Logger.LogWarning("Failed to update consonant");

            _consonants = await RefreshConsonants();
            StateHasChanged();
        }

        private async Task CommitVowelChangesAsync(Vowel arg)
        {
            var response = await Client.PutAsJsonAsync("api/v1/Builder/Vowel", arg);
            if (response.IsSuccessStatusCode)
                Logger.LogInformation("Vowel updated successfully");
            else
                Logger.LogWarning("Failed to update vowel");

            _vowels = await RefreshVowels();
            StateHasChanged();
        }

        private async Task RemoveConsonantAsync(Consonant deleteCtxItem)
        {
            var response = await Client.DeleteAsync($"api/v1/Builder/Consonant?phonemeId={deleteCtxItem.PhonemeID}");
            if (!response.IsSuccessStatusCode)
                Logger.LogWarning("Failed to delete consonant");
            else
                Logger.LogInformation("Consonant deleted successfully");

            _consonants = await RefreshConsonants();
            StateHasChanged();
        }

        private async Task RemoveVowelAsync(Vowel deleteCtxItem)
        {
            var response = await Client.DeleteAsync($"api/v1/Builder/Vowel?phonemeId={deleteCtxItem.PhonemeID}");
            if (!response.IsSuccessStatusCode)
                Logger.LogWarning("Failed to delete vowel");
            else
                Logger.LogInformation("Vowel deleted successfully");
            _vowels = await RefreshVowels();
            StateHasChanged();
        }
    }
}