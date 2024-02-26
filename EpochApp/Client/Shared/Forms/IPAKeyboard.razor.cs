using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared.Forms
{
    public partial class IPAKeyboard
    {
        public enum KeyboardType
        {
            Phoneme,
            Consonant,
            Vowel
        }

        private List<Consonant> _consonants = new List<Consonant>();

        private string _ipa;
        private List<Vowel> _vowels = new List<Vowel>();
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