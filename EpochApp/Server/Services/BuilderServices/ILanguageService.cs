// EpochWorlds
// ILanguageService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Shared;
using EpochApp.Shared.Config;

namespace EpochApp.Server.Services
{
    /// <summary>
    ///     Service contract for generating language content.
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        ///     Gets dictionary words that are used in the language generation.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="IEnumerable{T}" /> where T is
        ///     <see cref="DictionaryWord" />.
        /// </returns>
        Task<List<DictionaryWord>> GetDictionaryWords();

        /// <summary>
        ///     Adds a dictionary word to the database.
        /// </summary>
        /// <param name="wordToAdd">
        ///     The <see cref="DictionaryWord" /> to add to the database.
        /// </param>
        /// <returns>
        ///     <see cref="Task" />
        /// </returns>
        Task AddDictionaryWord(DictionaryWord wordToAdd);

        /// <summary>
        ///     Updates a dictionary word in the database.
        /// </summary>
        /// <param name="wordToUpdate">
        ///     The <see cref="DictionaryWord" /> to update in the database.
        /// </param>
        /// <returns>
        ///     <see cref="Task" />
        /// </returns>
        Task UpdateDictionaryWord(DictionaryWord wordToUpdate);

        /// <summary>
        ///     Removes a dictionary word from the database.
        /// </summary>
        /// <param name="wordToRemove">
        ///     The <see cref="DictionaryWord" /> to remove from the database.
        /// </param>
        /// <returns>
        ///     <see cref="Task" />
        /// </returns>
        Task RemoveDictionaryWord(DictionaryWord wordToRemove);

        /// <summary>
        ///     Gets parts of speech lookups
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="IEnumerable{T}" /> where T is <see cref="PartOfSpeech" />.
        /// </returns>
        Task<List<PartOfSpeech>> GetPartsOfSpeech();

        /// <summary>
        ///     Generate a language based on the given language settings.
        /// </summary>
        /// <param name="content">
        ///     The <see cref="BuilderContent" /> that contains the <see cref="ConstructedLanguage" /> settings to be deserialized.
        /// </param>
        /// <returns>
        ///     <see cref="ConstructedLanguageResult" />
        /// </returns>
        Task GenerateLanguage(BuilderContent content);
    }
}