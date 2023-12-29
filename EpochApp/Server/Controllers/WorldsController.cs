using EpochApp.Server.Data;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorldsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        public WorldsController(EpochDataDbContext context)
        {
            _context = context;
        }

        // GET: api/Worlds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<World>>> GetWorlds()
        {
            return await _context.Worlds.ToListAsync();
        }

        [HttpGet("User/{ownerId}")]
        public async Task<ActionResult<IEnumerable<World>>> GetUserWorlds(Guid ownerId)
        {
            return await _context.Worlds
                                 .Where(x => x.OwnerID == ownerId)
                                 .Include(x => x.CurrentWorldDate)
                                 .Include(x => x.MetaData)
                                 .ToListAsync();
        }

        // GET: api/Worlds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<World>> GetWorld(Guid id)
        {
            var world = await _context.Worlds.FindAsync(id);

            if (world == null)
            {
                return NotFound();
            }

            return world;
        }

        // PUT: api/Worlds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorld(Guid id, World world)
        {
            if (id != world.WorldID)
            {
                return BadRequest();
            }

            world.DateModified = DateTime.Now;
            _context.Entry(world).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/Worlds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<World>> PostWorld(World world)
        {
            world.DateCreated = DateTime.Now;
            _context.Worlds.Add(world);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorld", new { id = world.WorldID }, world);
        }

        // DELETE: api/Worlds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorld(Guid id)
        {
            var world = await _context.Worlds.FindAsync(id);
            if (world == null)
            {
                return NotFound();
            }

            _context.Worlds.Remove(world);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorldExists(Guid id)
        {
            return _context.Worlds.Any(e => e.WorldID == id);
        }
    }
}