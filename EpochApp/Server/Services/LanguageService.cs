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
using Microsoft.IdentityModel.Tokens;
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
            var grammar = language.Syntax;
            var vocab = language.Vocabulary;

            if (!string.IsNullOrWhiteSpace(language.NativePronunciation))
                langResult.Pronunciation = language.NativePronunciation;
            else
                langResult.Pronunciation = await GenerateWordAsync(phonology);
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
            language.NativePronunciation = langNameWord.IPA;
            langResult.Words.Add(langNameWord);

            // Generate words from dictionary
            _logger.LogInformation("Generating dictionary words...");
            await GenerateDictionaryWordsAsync(phonology, spelling, langResult);
            _logger.LogInformation("Finished generating dictionary words!");

            // Save changes to language settings and generated content
            _logger.LogInformation("Saving changes to database...");
            var xmlLang = await _serialization.SerializeToXmlAsync(language);
            var xmlGen = await _serialization.SerializeToXmlAsync(langResult);
            content.ContentXml = xmlLang;
            content.GeneratedXml = xmlGen;
            content.DateModified = DateTime.UtcNow;

            // Save generated content to db
            _context.Entry(content).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Changes saved!");
        }

        public async Task AddConsonantAsync(Consonant consonant)
        {
            await _context.Consonants.AddAsync(consonant);
            await _context.SaveChangesAsync();
        }

        public async Task AddVowelAsync(Vowel vowel)
        {
            await _context.Vowels.AddAsync(vowel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateConsonantAsync(Consonant consonant)
        {
            var cons = await _context.Consonants.ToListAsync();
            if (cons.Contains(consonant))
            {
                _context.Consonants.Update(consonant);
                _context.Entry(consonant).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddConsonantAsync(consonant);
            }
        }

        public async Task UpdateVowelAsync(Vowel vowel)
        {
            var vowels = await _context.Vowels.ToListAsync();
            if (vowels.Contains(vowel))
            {
                _context.Vowels.Update(vowel);
                _context.Entry(vowel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                await AddVowelAsync(vowel);
            }
        }

        public async Task RemoveConsonantAsync(Consonant consonant)
        {
            _context.Consonants.Remove(consonant);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveVowelAsync(Vowel vowel)
        {
            _context.Vowels.Remove(vowel);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<PartOfSpeech>> GetPartsOfSpeech()
        {
            var pos = await _context.PartsOfSpeech.ToListAsync();
            return await Task.FromResult(pos);
        }

        private async Task<string> GenerateWordAsync(Phonology phonology)
        {
            var consonants = new List<string>();
            var initConsts = new List<string>();
            var medConsts = new List<string>();
            var finalConsts = new List<string>();
            var vowels = new List<string>();
            var hVowels = new List<string>();// Harmonic vowels

            if (!phonology.UseIntermediateWordStructure && !phonology.UseAdvancedWordStructure)
            {
                if (phonology.Consonants.IsNullOrEmpty())
                {
                    consonants = await GetRandomConsonantsAsync(random.Next(7, 15));
                }
                else
                {
                    consonants = phonology.Consonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                    consonants.ForEach(x => x = x.Trim());
                }
                if (phonology.Vowels.IsNullOrEmpty())
                {
                    vowels = await GetRandomVowelsAsync(random.Next(3, 8));
                }
                else
                {
                    vowels = phonology.Vowels.Split(",", StringSplitOptions.TrimEntries).ToList();
                    vowels.ForEach(x => x = x.Trim());
                }
                var consonantsBuilder = new StringBuilder();
                consonants.ForEach(x =>
                {
                    consonantsBuilder.Append(x);
                    consonantsBuilder.Append(", ");
                });
                var vowelsBuilder = new StringBuilder();
                vowels.ForEach(x =>
                {
                    vowelsBuilder.Append(x);
                    vowelsBuilder.Append(", ");
                });
                phonology.Consonants = consonantsBuilder.ToString();
                phonology.Vowels = vowelsBuilder.ToString();
                return await GenerateWordAsync(consonants, vowels, phonology);
            }
            if (phonology.UseIntermediateWordStructure && !phonology.UseAdvancedWordStructure)
            {
                initConsts = phonology.InitialConsonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                initConsts.ForEach(x => x = x?.Trim());
                medConsts = phonology.MedialConsonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                medConsts.ForEach(x => x = x?.Trim());
                finalConsts = phonology.FinalConsonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                finalConsts.ForEach(x => x = x?.Trim());
                vowels = phonology.Vowels.Split(",", StringSplitOptions.TrimEntries).ToList();
                vowels.ForEach(x => x = x.Trim());
                if (phonology.UseVowelHarmony)
                {
                    hVowels = phonology.HarmonicVowels.Split(",").ToList();
                    hVowels.ForEach(x => x = x.Trim());
                }

                return await Task.FromResult("");
            }
            return await Task.FromResult("");
        }

        private async Task GenerateDictionaryWordsAsync(Phonology phonology, Spelling spelling, ConstructedLanguageResult langResult)
        {
            var consonants = new List<string>();
            var initConsts = new List<string>();
            var medConsts = new List<string>();
            var finalConsts = new List<string>();
            var vowels = new List<string>();
            var hVowels = new List<string>();// Harmonic vowels

            if (!phonology.UseIntermediateWordStructure && !phonology.UseAdvancedWordStructure)
            {
                if (phonology.Consonants.IsNullOrEmpty())
                {
                    consonants = await GetRandomConsonantsAsync(random.Next(7, 15));
                }
                else
                {
                    consonants = phonology.Consonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                    consonants.ForEach(x => x = x.Trim());
                }
                if (phonology.Vowels.IsNullOrEmpty())
                {
                    vowels = await GetRandomVowelsAsync(random.Next(3, 8));
                }
                else
                {
                    vowels = phonology.Vowels.Split(",", StringSplitOptions.TrimEntries).ToList();
                    vowels.ForEach(x => x = x.Trim());
                }
                var consonantsBuilder = new StringBuilder();
                consonants.ForEach(x =>
                {
                    consonantsBuilder.Append(x);
                    consonantsBuilder.Append(", ");
                });
                var vowelsBuilder = new StringBuilder();
                vowels.ForEach(x =>
                {
                    vowelsBuilder.Append(x);
                    vowelsBuilder.Append(", ");
                });
                phonology.Consonants = consonantsBuilder.ToString();
                phonology.Vowels = vowelsBuilder.ToString();
            }
            else if (phonology.UseIntermediateWordStructure && !phonology.UseAdvancedWordStructure)
            {
                initConsts = phonology.InitialConsonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                initConsts.ForEach(x => x = x?.Trim());
                medConsts = phonology.MedialConsonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                medConsts.ForEach(x => x = x?.Trim());
                finalConsts = phonology.FinalConsonants.Split(",", StringSplitOptions.TrimEntries).ToList();
                finalConsts.ForEach(x => x = x?.Trim());
                vowels = phonology.Vowels.Split(",", StringSplitOptions.TrimEntries).ToList();
                vowels.ForEach(x => x = x.Trim());
                if (phonology.UseVowelHarmony)
                {
                    hVowels = phonology.HarmonicVowels.Split(",").ToList();
                    hVowels.ForEach(x => x = x.Trim());
                }
            }

            var words = await GetDictionaryWords();
            foreach (var word in words)
            {
                _logger.LogInformation($"Generating Dictionary Word: {word.Translations}");
                var ipa = "";
                if (!phonology.UseIntermediateWordStructure && !phonology.UseAdvancedWordStructure)
                    ipa = await GenerateWordAsync(consonants, vowels, phonology);
                var conLangWord = await ApplySpellingRulesAsync(ipa, spelling);
                var altWord = "";
                if (spelling.UseSecondSpelling)
                    altWord = await ApplyAltSpellingRulesAsync(ipa, spelling);
                var generatedWord = new GeneratedWord
                                    {
                                        Translations = word.Translations,
                                        PartOfSpeech = word.PartOfSpeech.Abbreviation,
                                        IPA = ipa,
                                        ConLangWord = conLangWord,
                                        ConLangWordAlt = altWord
                                    };
                _logger.LogInformation($"Generated word: {generatedWord.Translations} with IPA {generatedWord.IPA}");
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
        }

        private async Task<string> GenerateWordAsync(List<string> consonants, List<string> vowels, Phonology phonology)
        {
            _logger.LogInformation("Generating simple word...");
            var sb = new StringBuilder();
            var startWithVowel = await ShouldStartWithVowelAsync(phonology);
            var endWithVowel = await ShouldEndWithVowelAsync(phonology);
            var wordSyllables = random.Next(1, 3);
            // Generate syllable
            if (startWithVowel)
            {
                var vowel = vowels[random.Next(0, vowels.Count - 1)];
                sb.Append(vowel);
                for (var i = 0; i < wordSyllables; i++)
                {
                    sb.Append(consonants[random.Next(0, consonants.Count - 1)]);
                    sb.Append(vowels[random.Next(0, vowels.Count - 1)]);
                }
                if (!endWithVowel)
                    sb.Append(consonants[random.Next(0, consonants.Count - 1)]);
            }
            else
            {
                for (var i = 0; i < wordSyllables; i++)
                {
                    sb.Append(consonants[random.Next(0, consonants.Count - 1)]);
                    sb.Append(vowels[random.Next(0, vowels.Count - 1)]);
                }
                if (!endWithVowel)
                    sb.Append(consonants[random.Next(0, consonants.Count - 1)]);
            }
            _logger.LogInformation("Word generated!");
            return await Task.FromResult(sb.ToString());
        }

        private async Task<bool> ShouldEndWithVowelAsync(Phonology phonology)
        {
            if (phonology.UseVowelProbabilities)
                return await IsProbable((int)phonology.VowelAtEndProbability);

            return await IsProbable(15);
        }

        private async Task<bool> ShouldStartWithVowelAsync(Phonology phonology)
        {
            if (phonology.UseVowelProbabilities)
                return await IsProbable((int)phonology.VowelAtStartProbability);

            return await IsProbable(35);
        }

        private Task<bool> IsProbable(int probability)
        {
            if (probability > 100)
                return Task.FromResult(true);

            var result = random.Next(1, 100);
            return Task.FromResult(result <= probability);
        }

        private async Task<TObject> GetRandomItem<TObject>(IEnumerable<TObject> objList) where TObject : class
        {
            var objArr = objList.ToArray();
            var randIndex = random.Next(0, objList.Count() - 1);
            return await Task.FromResult(objArr[randIndex]);
        }

        private async Task<List<string>> GetRandomVowelsAsync(int vowelCount)
        {
            var vowelsDb = await GetVowelsAsync();
            var vowels = new List<string>();
            for (var i = 0; i < vowelCount; i++)
            {
                var randVowel = random.Next(0, vowelsDb.Count - 1);
                var vowel = vowelsDb[randVowel];
                vowels.Add(vowel.PhonemeChar);
            }
            return await Task.FromResult(vowels);
        }

        private async Task<List<string>> GetRandomConsonantsAsync(int consonantCount)
        {
            var consonantsDb = await GetConsonantsAsync();
            var consonants = new List<string>();
            for (var i = 0; i < consonantCount; i++)
            {
                var randConsonant = random.Next(0, consonantsDb.Count - 1);
                var consonant = consonantsDb[randConsonant];
                consonants.Add(consonant.PhonemeChar);
            }
            return await Task.FromResult(consonants);
        }

        public async Task<List<Consonant>> GetConsonantsAsync()
        {
            var cons = await _context.Consonants.ToListAsync();
            return await Task.FromResult(cons);
        }

        public async Task<List<Vowel>> GetVowelsAsync()
        {
            var vowels = await _context.Vowels.ToListAsync();
            return await Task.FromResult(vowels);
        }

        #region Dictionary

        /// <inheritdoc />
        public async Task<List<DictionaryWord>> GetDictionaryWords()
        {
            var words = await _context.DictionaryWords
                                      .Include(x => x.PartOfSpeech)
                                      .ToListAsync();
            return words;
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