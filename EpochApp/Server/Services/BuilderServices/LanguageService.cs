// EpochWorlds
// LanguageService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 22-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using EpochApp.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EpochApp.Server.Services
{

    /// <summary>
    ///     Service for generating language content.
    /// </summary>
    public class LanguageService : ILanguageService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<ILanguageService> _logger;
        private readonly ISerializationService _serialization;
        private Random random = new Random();

        /// <summary>
        ///     Constructor for the language service.
        /// </summary>
        /// <param name="context"> The database context </param>
        /// <param name="logger">
        ///     <see cref="ILogger" /> dependency
        /// </param>
        /// <param name="serialization">
        ///     The <see cref="ISerializationService" /> dependency
        /// </param>
        public LanguageService(EpochDataDbContext context, ILogger<LanguageService> logger, ISerializationService serialization)
        {
            _context = context;
            _logger = logger;
            _serialization = serialization;
        }

        /// <inheritdoc />
        public async Task<List<PartOfSpeech>> GetPartsOfSpeech()
        {
            var pos = await _context.PartsOfSpeech.ToListAsync();
            return await Task.FromResult(pos);
        }

        /// <inheritdoc />
        public async Task GenerateLanguage(BuilderContent content)
        {
            if (content.ContentType != ContentType.ConstructedLanguage)
                return;

            var langResult = new ConstructedLanguageResult
                             {
                                 Author = content.AuthorID,
                                 Words = new List<GeneratedWord>()
                             };
            ConstructedLanguage language = null!;
            try
            {
                _logger.LogWarning("Deserialization of Constructed Language...");
                language = await _serialization.DeserializeFromXmlAsync<ConstructedLanguage>(content.ContentXml);
                _logger.LogInformation("Deserialization success!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return;
            }


            var phonology = language.Phonology;
            var spelling = language.Spelling;
            var grammar = language.Grammar;
            var vocab = language.Vocabulary;

            if (!string.IsNullOrWhiteSpace(language.NativePronunciation))
                langResult.Pronunciation = language.NativePronunciation;
            else
                langResult.Pronunciation = await GenerateLangWordIPAAsync(phonology);
            if (!string.IsNullOrWhiteSpace(language.LangName))
                langResult.LanguageName = language.LangName;
            else
                langResult.LanguageName = await ApplySpellingRulesAsync(langResult.Pronunciation, spelling);
            var langNameWord = new GeneratedWord
                               {
                                   Translations = langResult.LanguageName,
                                   PartOfSpeech = "n.",
                                   IPA = langResult.Pronunciation,
                                   ConLangWord = langResult.LanguageName,
                                   ConLangWordAlt = await ApplyAltSpellingRulesAsync(langResult.Pronunciation, spelling)
                               };
            langResult.Words.Add(langNameWord);

            // Generate words from dictionary
            await GenerateDictionaryWordsAsync(phonology, spelling, langResult);


            await Task.CompletedTask;
        }

        private async Task GenerateDictionaryWordsAsync(Phonology phonology, Spelling spelling, ConstructedLanguageResult langResult)
        {
            var words = await GetDictionaryWords();
            foreach (var word in words)
            {
                var ipa = await GenerateLangWordIPAAsync(phonology);
                var conlangWord = await ApplySpellingRulesAsync(ipa, spelling);
                var altWord = "";
                if (spelling.UseSecondSpelling)
                    altWord = await ApplyAltSpellingRulesAsync(ipa, spelling);
                var generatedWord = new GeneratedWord
                                    {
                                        Translations = word.Translations,
                                        PartOfSpeech = word.PartOfSpeech.Abbreviation,
                                        IPA = ipa,
                                        ConLangWord = conlangWord,
                                        ConLangWordAlt = altWord
                                    };
                langResult.Words.Add(generatedWord);
            }
        }

        private async Task<string> ApplyAltSpellingRulesAsync(string ipa, Spelling spelling)
        {
            return null;
        }

        private async Task<string> ApplySpellingRulesAsync(string ipa, Spelling spelling)
        {
            if (spelling.UseSpellingRules)
            {
                return "conlang word here...";
            }
            return ipa;
            return null;
        }

        private async Task<string> GenerateLangWordIPAAsync(Phonology phonology)
        {
            var sb = new StringBuilder();

            sb.Append("random ipa string here...");

            return await Task.FromResult(sb.ToString());
        }

        #region Dictionary

        /// <inheritdoc />
        public async Task<List<DictionaryWord>> GetDictionaryWords()
        {
            var words = await _context.DictionaryWords
                                      .Include(x => x.PartOfSpeech)
                                      .ToListAsync();
            return await Task.FromResult(words);
        }

        /// <inheritdoc />
        public async Task AddDictionaryWord(DictionaryWord wordToAdd)
        {
            await _context.DictionaryWords.AddAsync(wordToAdd);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateDictionaryWord(DictionaryWord wordToUpdate)
        {
            var dictionaryWords = await _context.DictionaryWords.ToListAsync();
            if (dictionaryWords.Contains(wordToUpdate))
            {
                _context.DictionaryWords.Update(wordToUpdate);
                _context.Entry(wordToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddDictionaryWord(wordToUpdate);
            }
        }

        /// <inheritdoc />
        public async Task RemoveDictionaryWord(DictionaryWord wordToRemove)
        {
            _context.DictionaryWords.Remove(wordToRemove);
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}