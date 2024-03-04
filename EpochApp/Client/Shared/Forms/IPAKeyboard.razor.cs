using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    ///     A keyboard for the International Phonetic Alphabet. To be used in certain areas where IPA is needed.
    /// </summary>
    public partial class IPAKeyboard
    {
        /// <summary>
        ///     The type of keyboard to display.
        /// </summary>
        public enum KeyboardType
        {
            /// <summary>
            ///     The phoneme keyboard. Displays both consonants and vowels.
            /// </summary>
            Phoneme,

            /// <summary>
            ///     The consonant keyboard. Displays only consonants.
            /// </summary>
            Consonant,

            /// <summary>
            ///     The vowel keyboard. Displays only vowels.
            /// </summary>
            Vowel
        }

        private List<Consonant> _consonants = new List<Consonant>();

        private string _ipa;
        private List<Vowel> _vowels = new List<Vowel>();

        /// <summary>
        ///     The type of keyboard to display.
        /// </summary>
        [Parameter] public KeyboardType Keyboard { get; set; } = KeyboardType.Phoneme;

        [Inject] private HttpClient Client { get; set; }
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Keyboard == KeyboardType.Phoneme || Keyboard == KeyboardType.Consonant)
                await RefreshConsonantsAsync();
            if (Keyboard == KeyboardType.Phoneme || Keyboard == KeyboardType.Vowel)
                await RefreshVowelsAsync();
        }
        private Task BackspaceIPAAsync()
        {
            if (_ipa.IsNullOrEmpty())
                return Task.CompletedTask;

            _ipa = _ipa.Remove(_ipa.Length - 1);
            return Task.CompletedTask;
        }
        private async Task<List<Vowel>> RefreshVowelsAsync()
        {
            _vowels = await Client.GetFromJsonAsync<List<Vowel>>("api/v1/Builder/Vowels");
            return _vowels;
        }
        private async Task<List<Consonant>> RefreshConsonantsAsync()
        {
            _consonants = await Client.GetFromJsonAsync<List<Consonant>>("api/v1/Builder/Consonants");
            return _consonants;
        }
        private Task AppendStringAsync(string ipa)
        {
            _ipa += ipa;
            return Task.CompletedTask;
        }
    }
}