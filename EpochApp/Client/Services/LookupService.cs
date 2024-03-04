// EpochWorlds
// LookupService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 3-3-2024
using EpochApp.Shared;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    /// <summary>
    ///   Service for handling and modifying Article Data.
    /// </summary>
    public class LookupService : ILookupService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ILookupService> _logger;

        /// <summary>
        ///   Constructor for LookupService.
        /// </summary>
        /// <param name="logger"> The logger. </param>
        /// <param name="client"> The http client. </param>
        public LookupService(ILogger<LookupService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        /// <inheritdoc />
        public async Task<List<ISOLanguage>> GetLanguagesAsync()
        {
            var languages = await _client.GetFromJsonAsync<List<ISOLanguage>>("api/v1/Lookups/lkLanguages");
            return await Task.FromResult(languages);
        }

        /// <inheritdoc />
        public async Task<List<Phoneme>> GetPhonemesAsync()
        {
            var phonemes = await _client.GetFromJsonAsync<List<Phoneme>>("api/v1/Lookups/lkPhonemes");
            return await Task.FromResult(phonemes);
        }

        /// <inheritdoc />
        public async Task<List<Consonant>> GetConsonantsAsync()
        {
            var consonants = await _client.GetFromJsonAsync<List<Consonant>>("api/v1/Lookups/lkConsonants");
            return await Task.FromResult(consonants);
        }

        /// <inheritdoc />
        public async Task<List<PartOfSpeech>> GetPartsOfSpeechAsync()
        {
            var partOfSpeech = await _client.GetFromJsonAsync<List<PartOfSpeech>>("api/v1/Lookups/lkPartsOfSpeech");
            return await Task.FromResult(partOfSpeech);
        }

        /// <inheritdoc />
        public async Task<List<Vowel>> GetVowelsAsync()
        {
            var vowels = await _client.GetFromJsonAsync<List<Vowel>>("api/v1/Lookups/lkVowels");
            return await Task.FromResult(vowels);
        }

        /// <inheritdoc />
        public async Task<List<DictionaryWord>> GetDictionaryWordsAsync()
        {
            var words = await _client.GetFromJsonAsync<List<DictionaryWord>>("api/v1/Lookups/lkDictionaryWords");
            return await Task.FromResult(words);
        }

        /// <inheritdoc />
        public async Task<List<ArticleCategory>> GetArticleCategoriesAsync()
        {
            var cats = await _client.GetFromJsonAsync<List<ArticleCategory>>("api/v1/Articles/Categories");
            return await Task.FromResult(cats);
        }

        /// <inheritdoc />
        public async Task<List<SocialMedia>> GetSocialMediasAsync()
        {
            var socials = await _client.GetFromJsonAsync<List<SocialMedia>>("api/v1/Lookups/lkSocials");
            return await Task.FromResult(socials);
        }

        /// <inheritdoc />
        public async Task<List<MetaCategory>> GetMetaAsync()
        {
            var metas = await _client.GetFromJsonAsync<List<MetaCategory>>("api/v1/Lookups/lkMeta");
            return await Task.FromResult(metas);
        }

        /// <inheritdoc />
        public async Task<List<MetaTemplate>> GetMetaTemplatesAsync()
        {
            var metas = await _client.GetFromJsonAsync<List<MetaTemplate>>("api/v1/Lookups/lkMetaTemplates");
            return await Task.FromResult(metas);
        }
    }
}