// EpochWorlds
// BuilderController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 9-2-2024
using EpochApp.Server.Data;
using EpochApp.Server.Services;
using EpochApp.Shared;
using EpochApp.Shared.Config;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     A controller for handling user builder requests.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BuilderController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly ILanguageService _language;
        private readonly ISerializationService _serializer;
        private readonly ILogger<BuilderController> _logger;

        /// <summary>
        /// Constructor for the <see cref="BuilderController" />.
        /// </summary>
        /// <param name="context"> The injected <see cref="EpochDataDbContext" /> to use for the controller. </param>
        /// <param name="serializer"> The injected <see cref="ISerializationService" /> to use for the controller. </param>
        /// <param name="language"> The injected <see cref="ILanguageService" /> to use for the controller. </param>
        /// <param name="logger"> The injected <see cref="ILogger{TCategoryName}"/> where TCategoryName is <see cref="BuilderController"/>. </param>
        public BuilderController(EpochDataDbContext context, ISerializationService serializer, ILanguageService language, ILogger<BuilderController> logger)
        {
            _context = context;
            _serializer = serializer;
            _language = language;
            _logger = logger;
        }

        /// <summary>
        ///     Get a builder content by its ID.
        /// </summary>
        /// <param name="contentId">
        ///     The ID of the content to retrieve.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpGet("Content")]
        public async Task<IActionResult> GetBuilderContent([FromQuery] Guid contentId)
        {
            var content = await _context.BuilderContents
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId);
            if (content == null)
                return NotFound("No content found.");

            return Ok(content);
        }

        /// <summary>
        ///     Get all builder content by a specific author.
        /// </summary>
        /// <param name="userId">
        ///     The ID of the user to retrieve content for.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpGet("ContentByAuthor/{userId:guid}")]
        public async Task<IActionResult> GetBuilderContentByAuthor(Guid userId)
        {
            var builderContents = await _context.BuilderContents
                                                .Where(x => x.AuthorID == userId)
                                                .Include(x => x.Author)
                                                .ToListAsync()
                                  ?? new List<BuilderContent>();
            return Ok(builderContents);
        }

        /// <summary>
        ///     Get all builder content by a specific world.
        /// </summary>
        /// <param name="worldId">
        ///     The ID of the world to retrieve content for.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpGet("ContentByWorld")]
        public async Task<IActionResult> GetBuilderContentByWorld([FromQuery] Guid worldId)
        {
            var content = await _context.BuilderContents
                                        .Where(x => x.WorldID == worldId)
                                        .ToListAsync();
            if (content.Count == 0)
                return NotFound("No content found.");

            return Ok(content);
        }

        [HttpGet("ContentByType")]
        public async Task<IActionResult> GetBuilderContentByType([FromQuery] Guid userId, [FromQuery] int contentType)
        {
            var content = await _context.BuilderContents
                                        .Where(x => x.AuthorID == userId && x.ContentType == (ContentType)contentType)
                                        .ToListAsync();
            if (content.Count == 0)
                return NotFound("No content found.");

            return Ok(content);
        }

        /// <summary>
        ///     Adds new builder content to the database.
        /// </summary>
        /// <param name="content">
        ///     The content to add to the database.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpPost("Content")]
        public async Task<IActionResult> CreateNewContentAsync(BuilderContent content)
        {
            // Add created content to database
            content.DateCreated = DateTime.Now;
            try
            {
                await _context.BuilderContents.AddAsync(content);
                await _context.SaveChangesAsync();
                // return created content with its id
                return CreatedAtAction("GetBuilderContent", new { contentId = content.ContentID }, content);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Content")]
        public async Task<IActionResult> UpdateContentAsync([FromQuery] Guid userId, [FromQuery] Guid contentId, [FromBody] BuilderContent content)
        {
            // Verify the this is the sending users content to update
            var contentToUpdate = await _context.BuilderContents
                                                .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (contentToUpdate == null)
                return NotFound("No content to update or you do not have permission to update this content.");

            contentToUpdate.ContentName = content.ContentName;
            contentToUpdate.ContentID = content.ContentID;
            contentToUpdate.ContentXml = content.ContentXml;
            contentToUpdate.ContentType = content.ContentType;
            contentToUpdate.DateCreated = content.DateCreated;
            contentToUpdate.DateModified = DateTime.Now;
            contentToUpdate.WorldID = content.WorldID;
            contentToUpdate.AuthorID = content.AuthorID;
            //TODO: Add IsPublic to BuilderContent
            // contentToUpdate.IsPublic = content.IsPublic;

            // Update content in database
            try
            {
                _context.BuilderContents.Update(contentToUpdate);
                await _context.SaveChangesAsync();
                return Ok(contentToUpdate);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Content")]
        public async Task<IActionResult> DeleteContentAsync([FromQuery] Guid userId, [FromQuery] Guid contentId)
        {
            // Verify the this is the sending users content to delete
            var contentToDelete = await _context.BuilderContents
                                                .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (contentToDelete == null)
                return NotFound("No content to delete or you do not have permission to delete this content.");

            // Delete content from database
            try
            {
                _context.BuilderContents.Remove(contentToDelete);
                await _context.SaveChangesAsync();
                return Ok("Content deleted.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GeneratedContent")]
        public async Task<IActionResult> GenerateContentAsync([FromQuery] Guid userId, [FromQuery] Guid contentId)
        {
            var content = await _context.BuilderContents
                                        .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (content == null)
                return NotFound("No content found or you do not have access.");

            switch (content.ContentType)
            {
                case ContentType.ConstructedLanguage:
                    _logger.LogInformation("Generating language...");
                    await _language.GenerateLanguage(content);
                    break;
                case ContentType.Character:
                    break;
                case ContentType.Map:
                    break;
                case ContentType.World:
                    break;
                case ContentType.Calendar:
                    break;
                case ContentType.Religion:
                    break;
                case ContentType.System:
                    break;
                default:
                    return NoContent();
            }

            return Ok(content);
        }

        /// <summary>
        ///     Get all parts of speech.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="IEnumerable{T}" /> where T is <see cref="PartOfSpeech" />.
        /// </returns>
        [HttpGet("PartOfSpeech")]
        public async Task<ActionResult<List<PartOfSpeech>>> GetPartsOfSpeech()
        {
            var partsOfSpeech = await _language.GetPartsOfSpeech();
            return Ok(partsOfSpeech);
        }

        /// <summary>
        ///     Get all dictionary words.
        /// </summary>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="IEnumerable{T}" /> where T is
        ///     <see cref="DictionaryWord" />.
        /// </returns>
        [HttpGet("Dictionary")]
        public async Task<ActionResult<List<DictionaryWord>>> GetDictionaryWords()
        {
            var words = await _language.GetDictionaryWords();
            return Ok(words);
        }

        /// <summary>
        ///     Adds a dictionary word to the database.
        /// </summary>
        /// <param name="wordToAdd">
        ///     The <see cref="DictionaryWord" /> to add to the database.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [Authorize(Roles = "ADMIN,INTERNAL")]
        [HttpPost("Dictionary")]
        public async Task<IActionResult> AddDictionaryWord(DictionaryWord wordToAdd)
        {
            await _language.AddDictionaryWord(wordToAdd);
            var words = await _language.GetDictionaryWords();
            return Ok(words);
        }

        /// <summary>
        ///     Updates a dictionary word in the database.
        /// </summary>
        /// <param name="wordToUpdate">
        ///     The <see cref="DictionaryWord" /> to update in the database.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [Authorize(Roles = "ADMIN,INTERNAL")]
        [HttpPut("Dictionary")]
        public async Task<IActionResult> UpdateDictionaryWord(DictionaryWord wordToUpdate)
        {
            var dictWord = await _context.DictionaryWords.Where(x => x.WordId == wordToUpdate.WordId)
                                         .FirstOrDefaultAsync();
            dictWord.Translations = wordToUpdate.Translations;
            dictWord.Category = wordToUpdate.Category;
            dictWord.PartOfSpeechId = wordToUpdate.PartOfSpeechId;
            _context.Entry(dictWord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        ///     Removes a dictionary word from the database.
        /// </summary>
        /// <param name="wordId">
        ///     The <see cref="DictionaryWord" />'s ID to remove from the database.
        /// </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [Authorize(Roles = "ADMIN,INTERNAL")]
        [HttpDelete("Dictionary")]
        public async Task<IActionResult> RemoveDictionaryWord([FromQuery] int wordId)
        {
            var wordToRemove = await _context.DictionaryWords.FirstOrDefaultAsync(x => x.WordId == wordId);
            await _language.RemoveDictionaryWord(wordToRemove);
            return Ok();
        }

        [HttpGet("Phonemes")]
        public async Task<List<Phoneme>> GetPhonemes()
        {
            var phonemes = await _context.Phonemes.ToListAsync();
            return await Task.FromResult(phonemes);
        }

        [HttpPost("Consonant")]
        public async Task<IActionResult> AddConsonantAsync(Consonant consonant)
        {
            await _language.AddConsonantAsync(consonant);
            return Ok();
        }

        [HttpPost("Vowel")]
        public async Task<IActionResult> AddVowelAsync(Vowel vowel)
        {
            await _language.AddVowelAsync(vowel);
            return Ok();
        }

        [HttpPut("Consonant")]
        public async Task<IActionResult> UpdateConsonantAsync(Consonant consonant)
        {
            var cons = await _context.Consonants.Where(x => x.PhonemeID == consonant.PhonemeID)
                                     .FirstOrDefaultAsync();
            cons.Manner = consonant.Manner;
            cons.Place = consonant.Place;
            cons.IsVoiced = consonant.IsVoiced;
            _context.Entry(cons).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Vowel")]
        public async Task<IActionResult> UpdateVowelAsync(Vowel vowel)
        {
            var vow = await _context.Vowels.Where(x => x.PhonemeID == vowel.PhonemeID)
                                    .FirstOrDefaultAsync();
            vow.Depth = vowel.Depth;
            vow.Verticality = vowel.Verticality;
            vow.IsRounded = vowel.IsRounded;
            _context.Entry(vow).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("Consonant")]
        public async Task<IActionResult> RemoveConsonantAsync([FromQuery] string phonemeId)
        {
            var consToRemove = await _context.Consonants.FirstOrDefaultAsync(x => x.PhonemeID == phonemeId);
            await _language.RemoveConsonantAsync(consToRemove);
            return Ok();
        }

        [HttpDelete("Vowel")]
        public async Task<IActionResult> RemoveVowelAsync([FromQuery] string phonemeId)
        {
            var vowel = await _context.Vowels.FirstOrDefaultAsync(x => x.PhonemeID == phonemeId);
            await _language.RemoveVowelAsync(vowel);
            return Ok();
        }

        [HttpGet("Consonants")]
        public async Task<ActionResult<List<Consonant>>> GetConsonantsAsync()
        {
            var cons = await _context.Consonants.ToListAsync();
            return Ok(cons);
        }

        [HttpGet("Vowels")]
        public async Task<ActionResult<List<Vowel>>> GetVowelsAsync()
        {
            var vowels = await _context.Vowels.ToListAsync();
            return Ok(vowels);
        }
    }
}