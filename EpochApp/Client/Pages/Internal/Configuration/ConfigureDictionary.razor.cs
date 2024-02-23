using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Internal.Configuration
{
    /// <summary>
    ///     Page to configure dictionary for constructed language words.
    /// </summary>
    [Authorize(Roles = "ADMIN,INTERNAL")]
    public partial class ConfigureDictionary
    {
        private List<DictionaryWord> _dictionaryWords = new List<DictionaryWord>();
        private List<PartOfSpeech> _partsOfSpeech = new List<PartOfSpeech>();
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Model = new DictionaryWord();
            _partsOfSpeech = await RefreshPartsOfSpeechAsync();
            _dictionaryWords = await RefreshDictionaryAsync();
        }
        private async Task<List<PartOfSpeech>> RefreshPartsOfSpeechAsync()
        {
            var pos = await Client.GetFromJsonAsync<List<PartOfSpeech>>("api/v1/Builder/PartOfSpeech");
            return pos;
        }
        private async Task<List<DictionaryWord>> RefreshDictionaryAsync()
        {
            var dict = await Client.GetFromJsonAsync<List<DictionaryWord>>("api/v1/Builder/Dictionary");
            return dict;
        }
        private async Task RemoveDictionaryWordAsync(DictionaryWord deleteCtxItem)
        {
            var response = await Client.DeleteAsync($"api/v1/Builder/Dictionary?wordId={deleteCtxItem.WordId}");
            if (response.IsSuccessStatusCode)
            {
                _dictionaryWords = await RefreshDictionaryAsync();
                StateHasChanged();
            }
        }
        private async Task CommitChangesAsync(DictionaryWord arg)
        {
            var response = await Client.PutAsJsonAsync("api/v1/Builder/Dictionary", arg);
            if (response.IsSuccessStatusCode)
            {
                _dictionaryWords = await RefreshDictionaryAsync();
                StateHasChanged();
            }
        }
        private async Task AddDictionaryWordAsync(EditContext arg)
        {
            var item = arg.Model as DictionaryWord;
            var response = await Client.PostAsJsonAsync("api/v1/Builder/Dictionary", item);
            if (response.IsSuccessStatusCode)
            {
                _dictionaryWords = await RefreshDictionaryAsync();
                Model = new DictionaryWord();
                StateHasChanged();
            }
        }
    }
}