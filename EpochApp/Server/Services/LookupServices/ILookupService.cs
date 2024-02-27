// EpochWorlds
// ILookupService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
using EpochApp.Shared.Config;

namespace EpochApp.Server.Services
{
    /// <summary>
    ///     Service for fetching lookup table data.
    /// </summary>
    public interface ILookupService
    {
        /// <summary> Get all languages. </summary>
        /// <returns>
        ///     A list of <see cref="ISOLanguage" />.
        /// </returns>
        Task<List<ISOLanguage>> GetLanguagesAsync();

        /// <summary> Get all phonemes. </summary>
        /// <returns>
        ///     A list of <see cref="Phoneme" />.
        /// </returns>
        Task<List<Phoneme>> GetPhonemesAsync();

        /// <summary> Get all consonants. </summary>
        /// <returns>
        ///     A list of <see cref="Consonant" />.
        /// </returns>
        Task<List<Consonant>> GetConsonantsAsync();

        /// <summary>
        ///     Get all parts of speech.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="PartOfSpeech" />.
        /// </returns>
        Task<List<PartOfSpeech>> GetPartsOfSpeechAsync();

        /// <summary> Get all vowels. </summary>
        /// <returns>
        ///     A list of <see cref="Vowel" />.
        /// </returns>
        Task<List<Vowel>> GetVowelsAsync();

        /// <summary>
        ///     Get all dictionary words.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="DictionaryWord" />.
        /// </returns>
        Task<List<DictionaryWord>> GetDictionaryWordsAsync();

        /// <summary>
        ///     Get all article categories.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="ArticleCategory" />.
        /// </returns>
        Task<List<ArticleCategory>> GetArticleCategoriesAsync();

        /// <summary>
        ///     Get all social media lookups.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="SocialMedia" />.
        /// </returns>
        Task<List<SocialMedia>> GetSocialMediasAsync();
    }
}