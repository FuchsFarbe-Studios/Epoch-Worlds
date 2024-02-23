using EpochApp.Server.Data;
using EpochApp.Server.Services.WorldService;
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
        private readonly IWorldService _worldService;

        /// <summary>
        ///     Constructor for WorldsController
        /// </summary>
        /// <param name="context"> Injected <see cref="EpochDataDbContext"/>. </param>
        /// <param name="worldService"> Injected <see cref="IWorldService"/>. </param>
        public WorldsController(EpochDataDbContext context, IWorldService worldService)
        {
            _context = context;
            _worldService = worldService;
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
                                       .Where(x => x.OwnerID == ownerId && (x.DateRemoved >= DateTime.Now || x.DateRemoved == null))
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
                                                        IsActiveWorld = x.IsActiveWorld.Value,
                                                        WorldDate = new WorldDateDTO
                                                                    {
                                                                        CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                                        CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                                        CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                                        CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                                        BeforeEra = x.CurrentWorldDate.BeforeEraName,
                                                                        AfterEra = x.CurrentWorldDate.AfterEraName,
                                                                        BeforeEraAbbreviation = x.CurrentWorldDate.BeforeEraAbbreviation,
                                                                        AfterEraAbbreviation = x.CurrentWorldDate.AfterEraAbbreviation,
                                                                        CurrentEra = x.CurrentWorldDate.CurrentAge
                                                                    }
                                                    })
                                       .ToListAsync();
            return Ok(worlds);
        }

        [HttpGet("MetaTemplates")]
        public async Task<IActionResult> IndexMetaTemplatesAsync()
        {
            var metaTemplates = await _worldService.IndexMetaTemplatesAsync();
            return Ok(metaTemplates);
        }

        [HttpGet("ActiveWorld")]
        public async Task<IActionResult> GetActiveUserWorld([FromQuery] Guid ownerId)
        {
            var activeWorld = await _context.Worlds
                                            .Where(x => x.OwnerID == ownerId && x.IsActiveWorld.Value == true && (x.DateRemoved >= DateTime.Now || x.DateRemoved == null))
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
                                                             IsActiveWorld = x.IsActiveWorld.Value,
                                                             WorldDate = new WorldDateDTO
                                                                         {
                                                                             CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                                             CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                                             CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                                             CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                                             BeforeEra = x.CurrentWorldDate.BeforeEraName,
                                                                             AfterEra = x.CurrentWorldDate.AfterEraName,
                                                                             BeforeEraAbbreviation = x.CurrentWorldDate.BeforeEraAbbreviation,
                                                                             AfterEraAbbreviation = x.CurrentWorldDate.AfterEraAbbreviation,
                                                                             CurrentEra = x.CurrentWorldDate.CurrentAge
                                                                         }
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
                                             .Where(x => x.OwnerID == active.AuthorID && x.IsActiveWorld.Value == true && (x.DateRemoved >= DateTime.Now || x.DateRemoved == null))
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
                                                              IsActiveWorld = x.IsActiveWorld.Value,
                                                              WorldDate = new WorldDateDTO
                                                                          {
                                                                              CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                                              CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                                              CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                                              CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                                              BeforeEra = x.CurrentWorldDate.BeforeEraName,
                                                                              AfterEra = x.CurrentWorldDate.AfterEraName,
                                                                              BeforeEraAbbreviation = x.CurrentWorldDate.BeforeEraAbbreviation,
                                                                              AfterEraAbbreviation = x.CurrentWorldDate.AfterEraAbbreviation,
                                                                              CurrentEra = x.CurrentWorldDate.CurrentAge
                                                                          }
                                                          })
                                             .FirstOrDefaultAsync();
            return Ok(updatedWorld);
        }

        [HttpGet("World/{ownerId:guid}/{worldId:guid}")]
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
                                                       IsActiveWorld = x.IsActiveWorld.Value,
                                                       WorldDate = new WorldDateDTO
                                                                   {
                                                                       CurrentDay = x.CurrentWorldDate.CurrentDay,
                                                                       CurrentMonth = x.CurrentWorldDate.CurrentMonth,
                                                                       CurrentYear = x.CurrentWorldDate.CurrentYear,
                                                                       CurrentAge = x.CurrentWorldDate.CurrentAge,
                                                                       BeforeEra = x.CurrentWorldDate.BeforeEraName,
                                                                       AfterEra = x.CurrentWorldDate.AfterEraName,
                                                                       BeforeEraAbbreviation = x.CurrentWorldDate.BeforeEraAbbreviation,
                                                                       AfterEraAbbreviation = x.CurrentWorldDate.AfterEraAbbreviation,
                                                                       CurrentEra = x.CurrentWorldDate.CurrentAge
                                                                   }
                                                   })
                                      .FirstOrDefaultAsync(x => x.WorldID == worldId && x.AuthorID == ownerId && (x.DateRemoved >= DateTime.Now || x.DateRemoved == null));

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
                return BadRequest();

            var userWorlds = await _context.Worlds
                                           .Where(x => x.OwnerID == updatedWorld.AuthorID && (x.DateRemoved >= DateTime.Now || x.DateRemoved == null))
                                           .ToListAsync();
            var world = await _context.Worlds
                                      .Where(x => x.WorldID == worldId && x.OwnerID == updatedWorld.AuthorID)
                                      .Include(x => x.Owner)
                                      .Include(x => x.CurrentWorldDate)
                                      .Include(x => x.MetaData)
                                      .FirstOrDefaultAsync();

            if (!userWorlds.Contains(world))
                return BadRequest("Not your world!");

            if (world == null)
            {
                world.DateModified = DateTime.Now;
                world.WorldName = updatedWorld.WorldName ?? "";
                world.Pronunciation = updatedWorld.Pronunciation ?? "";
                world.Description = updatedWorld.Description ?? "";
                world.IsActiveWorld = updatedWorld.IsActiveWorld ?? false;
                world.MetaData = updatedWorld.MetaData;
            }
            if (world.CurrentWorldDate == null)
            {
                world.CurrentWorldDate = new WorldDate
                                         {
                                             CurrentDay = updatedWorld.CurrentDay ?? 1,
                                             CurrentMonth = updatedWorld.CurrentMonth ?? 1,
                                             CurrentYear = updatedWorld.CurrentYear ?? 1,
                                             CurrentAge = updatedWorld.WorldDate.CurrentAge ?? "Age",
                                             BeforeEraAbbreviation = updatedWorld.WorldDate.BeforeEraAbbreviation ?? "",
                                             AfterEraAbbreviation = updatedWorld.WorldDate.AfterEraAbbreviation ?? "",
                                             BeforeEraName = updatedWorld.WorldDate.BeforeEra ?? "",
                                             AfterEraName = updatedWorld.WorldDate.AfterEra ?? ""
                                         };
            }
            else
            {
                world.CurrentWorldDate.CurrentDay = updatedWorld.WorldDate.CurrentDay;
                world.CurrentWorldDate.CurrentMonth = updatedWorld.WorldDate.CurrentMonth;
                world.CurrentWorldDate.CurrentYear = updatedWorld.WorldDate.CurrentYear;
                world.CurrentWorldDate.CurrentAge = updatedWorld.WorldDate.CurrentAge;
                world.CurrentWorldDate.BeforeEraName = updatedWorld.WorldDate.BeforeEra;
                world.CurrentWorldDate.BeforeEraAbbreviation = updatedWorld.WorldDate.BeforeEraAbbreviation;
                world.CurrentWorldDate.AfterEraName = updatedWorld.WorldDate.AfterEra;
                world.CurrentWorldDate.AfterEraAbbreviation = updatedWorld.WorldDate.AfterEraAbbreviation;
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

        [HttpGet("WorldDate")]
        public async Task<ActionResult<WorldDateDTO>> GetWorldDateAsync([FromQuery] Guid worldId)
        {
            var worldDate = await _worldService.GetWorldDate(worldId);
            return Ok(worldDate);
        }

        [HttpPut("WorldDate")]
        public async Task<IActionResult> UpdateWorldDateAsync([FromQuery] Guid userId, [FromQuery] Guid worldId, WorldDateDTO dateDto)
        {
            var dateObject = await _worldService.GetWorldDate(worldId);
            var user = await _context.Users
                                     .Include(x => x.OwnedWorlds)
                                     .ThenInclude(x => x.CurrentWorldDate)
                                     .FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
                return BadRequest();

            var world = user.OwnedWorlds.FirstOrDefault(x => x.WorldID == worldId);
            if (world == null)
                return BadRequest();
            if (world.CurrentWorldDate.WorldID != dateObject.WorldId)
                return BadRequest();

            dateObject.CurrentDay = dateDto.CurrentDay;
            dateObject.CurrentMonth = dateDto.CurrentMonth;
            dateObject.CurrentYear = dateDto.CurrentYear;
            dateObject.CurrentAge = dateDto.CurrentAge;
            dateObject.BeforeEra = dateDto.BeforeEra;
            dateObject.AfterEra = dateDto.AfterEra;
            dateObject.BeforeEraAbbreviation = dateDto.BeforeEraAbbreviation;
            dateObject.AfterEraAbbreviation = dateDto.AfterEraAbbreviation;
            dateObject.CurrentEra = dateDto.CurrentEra;
            _context.Entry(dateObject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
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

        /// <summary> Deletes a world. </summary>
        /// <param name="ownerId"> The owner's ID. </param>
        /// <param name="worldId"> The world's ID. </param>
        /// <returns>
        ///     <see cref="IActionResult" />
        /// </returns>
        [HttpDelete("{ownerId:guid}/{worldId:guid}")]
        public async Task<IActionResult> DeleteWorld(Guid ownerId, Guid worldId)
        {
            var world = await _context.Worlds
                                      .Where(x => x.OwnerID == ownerId && x.WorldID == worldId)
                                      .Include(world => world.CurrentWorldDate)
                                      .Include(world => world.MetaData)
                                      .FirstOrDefaultAsync();
            if (world == null)
                return NotFound("World not found!");

            world.DateRemoved = DateTime.Now;
            _context.Entry(world).State = EntityState.Modified;
            // _context.Worlds.Remove(world);
            await _context.SaveChangesAsync();
            var dto = new WorldDTO
                      {
                          AuthorID = world.OwnerID,
                          WorldID = world.WorldID,
                          WorldName = world.WorldName,
                          Pronunciation = world.Pronunciation,
                          Description = world.Description,
                          DateCreated = world.DateCreated,
                          DateModified = world.DateModified,
                          DateRemoved = world.DateRemoved.Value,
                          CurrentDay = world?.CurrentWorldDate?.CurrentDay,
                          CurrentMonth = world?.CurrentWorldDate?.CurrentMonth,
                          CurrentYear = world?.CurrentWorldDate?.CurrentYear,
                          CurrentAge = world?.CurrentWorldDate?.CurrentAge,
                          MetaData = world?.MetaData,
                          IsActiveWorld = world.IsActiveWorld
                      };
            return Ok(dto);
        }

        private bool WorldExists(Guid id)
        {
            return _context.Worlds.Any(e => e.WorldID == id);
        }
    }
}