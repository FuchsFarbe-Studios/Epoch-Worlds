// EpochWorlds
// NewWorldsController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 27-2-2024
using EpochApp.Server.Data;
using EpochApp.Server.Services.WorldService;
using EpochApp.Shared;
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

        [HttpGet]
        public async Task<IActionResult> IndexWorldsAsync()
        {
            var worlds = await _worldService.GetWorldsAsync();
            return Ok(worlds);
        }

        [HttpGet("UserWorlds")]
        public async Task<IActionResult> IndexUserWorldsAsync([FromQuery] Guid userId)
        {
            var worlds = await _worldService.GetUserWorldsAsync(userId);
            return Ok(worlds);
        }

        [HttpGet("{worldId:guid}")]
        public async Task<IActionResult> GetWorldAsync(Guid worldId)
        {
            var world = await _worldService.GetWorldAsync(worldId);
            return Ok(world);
        }

        [HttpGet("View/{worldId:guid}")]
        public async Task<IActionResult> GetWorldViewAsync(Guid worldId)
        {
            var world = await _worldService.GetWorldViewAsync(worldId);
            return Ok(world);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorldAsync([FromBody] UserWorldDTO world)
        {
            var newWorld = await _worldService.CreateWorldAsync(world);
            return Ok(newWorld);
        }

        [HttpPut("{worldId:guid}")]
        public async Task<IActionResult> UpdateWorldAsync(Guid worldId, [FromBody] UserWorldDTO world)
        {
            var updatedWorld = await _worldService.UpdateWorldAsync(world);
            return Ok(updatedWorld);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveWorldAsync([FromQuery] Guid userId, [FromQuery] Guid worldId)
        {
            var world = await _worldService.DeleteWorldAsync(userId, worldId);
            if (world == null)
                return Unauthorized();

            return Ok(world);
        }
    }
}