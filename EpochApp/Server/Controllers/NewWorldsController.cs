// EpochWorlds
// NewWorldsController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable NotAccessedField.Local

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     Worlds Controller for managing user worlds.
    /// </summary>
    [Route("api/v2/Worlds")]
    [ApiController]
    public class NewWorldsController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly IWorldService _worldService;

        /// <summary>
        ///     Constructor for WorldsController
        /// </summary>
        /// <param name="context">
        ///     Injected <see cref="EpochDataDbContext" />.
        /// </param>
        /// <param name="worldService">
        ///     Injected <see cref="IWorldService" />.
        /// </param>
        public NewWorldsController(EpochDataDbContext context, IWorldService worldService)
        {
            _context = context;
            _worldService = worldService;
        }

        /// <summary>
        ///   Get all worlds.
        /// </summary>
        /// <returns> A list of <see cref="UserWorldDTO"/>. </returns>
        [HttpGet]
        public async Task<IActionResult> IndexWorldsAsync()
        {
            var worlds = await _worldService.GetWorldsAsync();
            return Ok(worlds);
        }

        /// <summary>
        ///  Get all worlds for a specific user.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> A list of <see cref="UserWorldDTO"/>. </returns>
        [HttpGet("UserWorlds")]
        public async Task<IActionResult> IndexUserWorldsAsync([FromQuery] Guid userId)
        {
            var worlds = await _worldService.GetUserWorldsAsync(userId);
            return Ok(worlds);
        }

        /// <summary>
        ///    Get a specific world.
        /// </summary>
        /// <param name="worldId"> The unique identifier for the world. </param>
        /// <returns> A <see cref="UserWorldDTO"/>. </returns>
        [HttpGet("{worldId:guid}")]
        public async Task<IActionResult> GetWorldAsync(Guid worldId)
        {
            var world = await _worldService.GetWorldAsync(worldId);
            return Ok(world);
        }

        /// <summary>
        ///   Get a specific world.
        /// </summary>
        /// <param name="worldId"> The unique identifier for the world. </param>
        /// <returns> A <see cref="World"/>. </returns>
        [HttpGet("View/{worldId:guid}")]
        public async Task<IActionResult> GetWorldViewAsync(Guid worldId)
        {
            var world = await _worldService.GetWorldViewAsync(worldId);
            return Ok(world);
        }

        /// <summary>
        ///  Get the active world for a user.
        /// </summary>
        /// <param name="ownerId"> The user's unique identifier. </param>
        /// <returns> A <see cref="UserWorldDTO"/>. </returns>
        [HttpGet("ActiveWorld")]
        public async Task<IActionResult> GetActiveUserWorld([FromQuery] Guid ownerId)
        {
            var activeWorld = await _worldService.GetActiveWorldAsync(ownerId);
            return Ok(activeWorld);
        }

        /// <summary>
        ///  Create a new world.
        /// </summary>
        /// <param name="world"> The world to create. </param>
        /// <returns> A <see cref="UserWorldDTO"/>. </returns>
        [HttpPost]
        public async Task<IActionResult> CreateWorldAsync([FromBody] UserWorldDTO world)
        {
            var newWorld = await _worldService.CreateWorldAsync(world);
            return Ok(newWorld);
        }

        /// <summary>
        ///    Update the active world for a user.
        /// </summary>
        /// <param name="active"> The world to make active. </param>
        /// <returns> A <see cref="UserWorldDTO"/>. </returns>
        [HttpPut("ActiveWorld")]
        public async Task<IActionResult> UpdateActiveUserWorlds([FromBody] UserWorldDTO active)
        {
            var updatedWorld = await _worldService.UpdateActiveUserWorldsAsync(active);
            return Ok(updatedWorld);
        }

        /// <summary>
        ///   Update a world.
        /// </summary>
        /// <param name="worldId"> The unique identifier for the world. </param>
        /// <param name="world"> The world to update. </param>
        /// <returns> A <see cref="UserWorldDTO"/>. </returns>
        [HttpPut("{worldId:guid}")]
        public async Task<IActionResult> UpdateWorldAsync(Guid worldId, [FromBody] UserWorldDTO world)
        {
            var updatedWorld = await _worldService.UpdateWorldAsync(world);
            return Ok(updatedWorld);
        }

        /// <summary>
        ///  Remove a world from a user's list of worlds.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="worldId"> The world's unique identifier. </param>
        /// <returns> A <see cref="UserWorldDTO"/>. </returns>
        [HttpDelete]
        public async Task<IActionResult> RemoveWorldAsync([FromQuery] Guid userId, [FromQuery] Guid worldId)
        {
            var world = await _worldService.DeleteWorldAsync(userId, worldId);
            if (world == null)
                return BadRequest("World not found.");

            return Ok(world);
        }

        private bool WorldExists(Guid id)
        {
            return _context.Worlds.Any(e => e.WorldId == id);
        }
    }
}