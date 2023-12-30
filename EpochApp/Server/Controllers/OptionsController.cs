using EpochApp.Server.Data;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     The options controller.
    /// </summary>
    /// <remarks>
    ///     Used for validating generation options, saving them to the database, and generating content.
    /// </remarks>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public OptionsController(EpochDataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentOptions>>> GetContentOptions()
        {
            return await _context.ContentOptions.ToListAsync();
        }

        /// <summary>
        ///     Gets the language options for a specified user.
        /// </summary>
        /// <param name="userId">
        ///     This user's language options.
        /// </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="IEnumerable{T}" /> where T is <see cref="LangOptions" />.
        /// </returns>
        [HttpGet("Options/Language/{userId}")]
        public async Task<ActionResult<IEnumerable<LangOptions>>> GetLangOptions(Guid userId)
        {
            return await _context.LangOptions.Where(x => x.OwnerID == userId)
                                 .Include(x => x.Phonology)
                                 .ToListAsync();
        }

        /// <summary>
        ///     Gets a particular language option for a specified user.
        /// </summary>
        /// <param name="userId">
        ///     User retrieving the option.
        /// </param>
        /// <param name="contentId"> Language option ID. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="ActionResult{TValue}" /> where TValue is
        ///     <see cref="LangOptions" />.
        /// </returns>
        [HttpGet("Options/Language/{userId}/{contentId}")]
        public async Task<ActionResult<LangOptions>> GetLangOptions(Guid userId, Guid contentId)
        {
            return await _context.LangOptions.Where(x => x.OwnerID == userId && x.OptionsID == contentId)
                                 .Include(x => x.Phonology)
                                 .FirstOrDefaultAsync();
        }

        [HttpGet("{userId}/{contentId}")]
        public async Task<ActionResult<ContentOptions>> GetContentOptions(Guid userId, Guid contentId)
        {
            var contentOptions = await _context.ContentOptions.FindAsync(new { userId, contentId });

            if (contentOptions == null)
                return NotFound();

            return contentOptions;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContentOptions(Guid id, ContentOptions contentOptions)
        {
            if (id != contentOptions.OptionsID)
                return BadRequest();

            _context.Entry(contentOptions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentOptionsExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        /// <summary>
        ///     Creates a new language option record in the database.
        /// </summary>
        /// <param name="contentOptions"> Language options. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="IActionResult" />.
        /// </returns>
        [HttpPost("Language")]
        public async Task<IActionResult> PostLangOptions(LangOptions contentOptions)
        {
            _context.LangOptions.Add(contentOptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLangOptions", new { userId = contentOptions.OwnerID, contentId = contentOptions.OptionsID }, contentOptions);
        }

        [HttpPost]
        public async Task<ActionResult<ContentOptions>> PostContentOptions(ContentOptions contentOptions)
        {
            _context.ContentOptions.Add(contentOptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContentOptions", new { id = contentOptions.OptionsID }, contentOptions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContentOptions(Guid id)
        {
            var contentOptions = await _context.ContentOptions.FindAsync(id);
            if (contentOptions == null)
            {
                return NotFound();
            }

            _context.ContentOptions.Remove(contentOptions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContentOptionsExists(Guid id)
        {
            return _context.ContentOptions.Any(e => e.OptionsID == id);
        }
    }
}