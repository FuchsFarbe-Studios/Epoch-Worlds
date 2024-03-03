// EpochWorlds
// LookupService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{

    /// <summary>
    ///     Service for fetching lookup table data.
    /// </summary>
    public class LookupService : ILookupService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<LookupService> _logger;

        public LookupService(EpochDataDbContext context, ILogger<LookupService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<ISOLanguage>> GetLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<Phoneme>> GetPhonemesAsync()
        {
            return await _context.Phonemes.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<Consonant>> GetConsonantsAsync()
        {
            return await _context.Consonants.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<PartOfSpeech>> GetPartsOfSpeechAsync()
        {
            return await _context.PartsOfSpeech.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<Vowel>> GetVowelsAsync()
        {
            return await _context.Vowels.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<DictionaryWord>> GetDictionaryWordsAsync()
        {
            return await _context.DictionaryWords.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<ArticleCategory>> GetArticleCategoriesAsync()
        {
            return await _context.ArticleCategories.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<SocialMedia>> GetSocialMediasAsync()
        {
            return await _context.SocialMedias.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<MetaCategory>> GetMetaAsync()
        {
            return await _context.MetaCategories.Include(x => x.Templates).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<MetaTemplate>> GetMetaTemplatesAsync()
        {
            return await _context.MetaTemplates.Include(x => x.Category).ToListAsync();
        }
    }
}