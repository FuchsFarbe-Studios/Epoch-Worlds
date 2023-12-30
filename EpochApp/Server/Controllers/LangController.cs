using EpochApp.Server.Data;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LangController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public LangController(EpochDataDbContext context)
        {
            _context = context;
        }

        // GET: api/Lang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentOptions>>> GetContentOptions()
        {
            return await _context.ContentOptions.ToListAsync();
        }

        [HttpGet("Options/Language/{userId}")]
        public async Task<ActionResult<IEnumerable<LangOptions>>> GetLangOptions(Guid userId)
        {
            return await _context.LangOptions.Where(x => x.OwnerID == userId)
                                 .Include(x => x.Phonology)
                                 .ToListAsync();
        }

        [HttpGet("Options/Language/{userId}/{contentId}")]
        public async Task<ActionResult<LangOptions>> GetLangOptions(Guid userId, Guid contentId)
        {
            return await _context.LangOptions.Where(x => x.OwnerID == userId && x.OptionsID == contentId)
                                 .Include(x => x.Phonology)
                                 .FirstOrDefaultAsync();
        }

        // GET: api/Lang/5
        [HttpGet("{userId}/{contentId}")]
        public async Task<ActionResult<ContentOptions>> GetContentOptions(Guid userId, Guid contentId)
        {
            var contentOptions = await _context.ContentOptions.FindAsync(new { userId, contentId });

            if (contentOptions == null)
                return NotFound();

            return contentOptions;
        }

        // PUT: api/Lang/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContentOptions(Guid id, ContentOptions contentOptions)
        {
            if (id != contentOptions.OptionsID)
            {
                return BadRequest();
            }

            _context.Entry(contentOptions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentOptionsExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Lang
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContentOptions>> PostContentOptions(ContentOptions contentOptions)
        {
            _context.ContentOptions.Add(contentOptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContentOptions", new { id = contentOptions.OptionsID }, contentOptions);
        }

        // DELETE: api/Lang/5
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