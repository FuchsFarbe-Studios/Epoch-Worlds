using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     Worlds Controller for managing user worlds.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorldsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;

        /// <summary>
        ///     Constructor for WorldsController
        /// </summary>
        /// <param name="context"> EpochDataDbContext </param>
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

        [HttpGet("User")]
        public async Task<ActionResult<IEnumerable<WorldDTO>>> GetUserWorlds([FromQuery] Guid ownerId)
        {
            var worlds = await _context.Worlds
                                       .Where(x => x.OwnerID == ownerId)
                                       .Include(x => x.CurrentWorldDate)
                                       .Include(x => x.MetaData)
                                       .Include(x => x.Owner)
                                       .Select(x => new WorldDTO
                                                    {
                                                        AuthorID = x.OwnerID,
                                                        WorldID = x.WorldID,
                                                        WorldName = x.WorldName,
                                                        Pronunciation = x.Pronunciation,
                                                        Description = x.Description,
                                                        DateCreated = x.DateCreated,
                                                        DateModified = x.DateModified,
                                                        DateRemoved = x.DateRemoved,
                                                        CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                        CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                        CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                        CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                        MetaData = x.MetaData,
                                                        IsActiveWorld = x.IsActiveWorld.Value
                                                    })
                                       .ToListAsync();
            return Ok(worlds);
        }

        [HttpGet("ActiveWorld")]
        public async Task<IActionResult> GetActiveUserWorld([FromQuery] Guid ownerId)
        {
            var activeWorld = await _context.Worlds
                                            .Where(x => x.OwnerID == ownerId && x.IsActiveWorld.Value == true)
                                            .Include(x => x.CurrentWorldDate)
                                            .Include(x => x.MetaData)
                                            .Include(x => x.Owner)
                                            .Select(x => new WorldDTO
                                                         {
                                                             AuthorID = x.OwnerID,
                                                             WorldID = x.WorldID,
                                                             WorldName = x.WorldName,
                                                             Pronunciation = x.Pronunciation,
                                                             Description = x.Description,
                                                             DateCreated = x.DateCreated,
                                                             DateModified = x.DateModified.Value,
                                                             DateRemoved = x.DateRemoved.Value,
                                                             CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                             CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                             CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                             CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                             MetaData = x.MetaData,
                                                             IsActiveWorld = x.IsActiveWorld.Value
                                                         })
                                            .FirstOrDefaultAsync();
            return Ok(activeWorld);
        }

        [HttpPut("ActiveWorld")]
        public async Task<IActionResult> UpdateActiveUserWorlds([FromBody] WorldDTO active)
        {
            var userWorlds = await _context.Worlds
                                           .Where(x => x.OwnerID == active.AuthorID)
                                           .ToListAsync();
            var activeWorld = await _context.Worlds
                                            .Where(x => x.OwnerID == active.AuthorID && x.WorldID == active.WorldID)
                                            .FirstOrDefaultAsync();
            foreach (var world in userWorlds)
            {
                if (world == activeWorld)
                    world.IsActiveWorld = true;
                else
                    world.IsActiveWorld = false;
                _context.Entry(world).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldExists(active.WorldID))
                {
                    return NotFound();
                }
                throw;
            }
            var updatedWorld = await _context.Worlds
                                             .Where(x => x.OwnerID == active.AuthorID && x.IsActiveWorld.Value == true)
                                             .Include(x => x.CurrentWorldDate)
                                             .Include(x => x.MetaData)
                                             .Select(x => new WorldDTO
                                                          {
                                                              AuthorID = x.OwnerID,
                                                              WorldID = x.WorldID,
                                                              WorldName = x.WorldName,
                                                              Pronunciation = x.Pronunciation,
                                                              Description = x.Description,
                                                              DateCreated = x.DateCreated,
                                                              DateModified = x.DateModified,
                                                              DateRemoved = x.DateRemoved,
                                                              CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                              CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                              CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                              CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                              MetaData = x.MetaData,
                                                              IsActiveWorld = x.IsActiveWorld.Value
                                                          })
                                             .FirstOrDefaultAsync();
            return Ok(updatedWorld);
        }

        [HttpGet("{ownerId:guid}/{worldId:guid}")]
        public async Task<ActionResult<WorldDTO>> GetWorld(Guid ownerId, Guid worldId)
        {
            var world = await _context.Worlds
                                      .Include(x => x.CurrentWorldDate)
                                      .Include(x => x.MetaData)
                                      .Include(x => x.Owner)
                                      .Select(x => new WorldDTO
                                                   {
                                                       AuthorID = x.OwnerID,
                                                       WorldID = x.WorldID,
                                                       WorldName = x.WorldName,
                                                       Pronunciation = x.Pronunciation,
                                                       Description = x.Description,
                                                       DateCreated = x.DateCreated,
                                                       DateModified = x.DateModified.Value,
                                                       DateRemoved = x.DateRemoved.Value,
                                                       CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                       CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                       CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                       CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                       MetaData = x.MetaData,
                                                       IsActiveWorld = x.IsActiveWorld.Value
                                                   })
                                      .FirstOrDefaultAsync(x => x.WorldID == worldId);

            if (world == null)
            {
                return NotFound();
            }

            return world;
        }

        [HttpPut("{worldId}")]
        public async Task<IActionResult> PutWorld(Guid worldId, WorldDTO updatedWorld)
        {
            if (worldId != updatedWorld.WorldID)
            {
                return BadRequest();
            }

            var world = await _context.Worlds
                                      .Where(x => x.WorldID == worldId && x.OwnerID == updatedWorld.AuthorID)
                                      .Include(x => x.Owner)
                                      .Include(x => x.CurrentWorldDate)
                                      .Include(x => x.MetaData)
                                      .FirstOrDefaultAsync();
            if (world == null)
            {
                world.DateModified = DateTime.Now;
                world.WorldName = updatedWorld.WorldName ?? "";
                world.Pronunciation = updatedWorld.Pronunciation ?? "";
                world.Description = updatedWorld.Description ?? "";
                world.IsActiveWorld = updatedWorld.IsActiveWorld ?? false;
                world.CurrentWorldDate.CurrentDay = updatedWorld.CurrentDay ?? 1;
                world.CurrentWorldDate.CurrentMonth = updatedWorld.CurrentMonth ?? 1;
                world.CurrentWorldDate.CurrentYear = updatedWorld.CurrentYear ?? 1;
                world.CurrentWorldDate.CurrentAge = updatedWorld.CurrentAge ?? "Age";
                world.MetaData = updatedWorld.MetaData;
            }
            _context.Entry(world).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldExists(worldId))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }


        [HttpPost("Create")]
        public async Task<IActionResult> PostWorld(WorldDTO world)
        {
            var newWorld = new World
                           {
                               OwnerID = world.AuthorID,
                               WorldName = world.WorldName,
                               Pronunciation = world.Pronunciation,
                               Description = world.Description,
                               IsActiveWorld = false,
                               DateCreated = DateTime.Now,
                               CurrentWorldDate = new WorldDate
                                                  {
                                                      CurrentDay = world.CurrentDay ?? 1,
                                                      CurrentMonth = world.CurrentMonth ?? 1,
                                                      CurrentYear = world.CurrentYear ?? 1,
                                                      CurrentAge = world.CurrentAge
                                                  },
                               MetaData = new List<WorldMeta>()
                           };
            _context.Worlds.Add(newWorld);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorld", new { ownerId = newWorld.OwnerID, worldId = newWorld.WorldID }, newWorld);
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