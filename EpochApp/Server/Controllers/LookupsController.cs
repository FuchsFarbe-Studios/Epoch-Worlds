using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using Microsoft.AspNetCore.Mvc;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     A controller for all lookup tables.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService, EpochDataDbContext context)
        {
            _lookupService = lookupService;
            _context = context;
        }

        /// <summary>
        ///   A get request for a list of all social media types.
        /// </summary>
        /// <returns> <see cref="Task{TResult}"/> where TResult is <see cref="IActionResult"/>. </returns>
        [HttpGet("lkSocials")]
        public async Task<IActionResult> GetSocialMediasAsync()
        {
            var socials = await _lookupService.GetSocialMediasAsync();
            return Ok(socials);
        }

        [HttpGet("lkArticleCategories")]
        public async Task<IActionResult> GetArticleCategoriesAsync()
        {
            var categories = await _lookupService.GetArticleCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("lkLanguages")]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            var languages = await _lookupService.GetLanguagesAsync();
            return Ok(languages);
        }

        [HttpGet("lkPhonemes")]
        public async Task<IActionResult> GetPhonemesAsync()
        {
            var phonemes = await _lookupService.GetPhonemesAsync();
            return Ok(phonemes);
        }

        [HttpGet("lkConsonants")]
        public async Task<IActionResult> GetConsonantsAsync()
        {
            var consonants = await _lookupService.GetConsonantsAsync();
            return Ok(consonants);
        }

        [HttpGet("lkPartsOfSpeech")]
        public async Task<IActionResult> GetPartsOfSpeechAsync()
        {
            var partsOfSpeech = await _lookupService.GetPartsOfSpeechAsync();
            return Ok(partsOfSpeech);
        }

        [HttpGet("lkVowels")]
        public async Task<IActionResult> GetVowelsAsync()
        {
            var vowels = await _lookupService.GetVowelsAsync();
            return Ok(vowels);
        }

        [HttpGet("lkDictionaryWords")]
        public async Task<IActionResult> GetDictionaryWordsAsync()
        {
            var dictionaryWords = await _lookupService.GetDictionaryWordsAsync();
            return Ok(dictionaryWords);
        }

        [HttpGet("lkMeta")]
        public async Task<ActionResult<List<MetaCategory>>> GetMetaAsync()
        {
            var meta = await _lookupService.GetMetaAsync();
            return Ok(meta);
        }

        [HttpGet("lkMetaTemplates")]
        public async Task<IActionResult> GetMetaTemplatesAsync()
        {
            return Ok(await _lookupService.GetMetaTemplatesAsync());
        }
    }
}