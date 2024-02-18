// EpochWorlds
// BuilderController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 9-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///     A controller for handling user builder requests.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuilderController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly ISerializationService _serializer;

        /// <summary>
        /// Constructor for the <see cref="BuilderController" />.
        /// </summary>
        /// <param name="context"> The injected <see cref="EpochDataDbContext" /> to use for the controller. </param>
        /// <param name="serializer"> The injected <see cref="ISerializationService" /> to use for the controller. </param>
        public BuilderController(EpochDataDbContext context, ISerializationService serializer)
        {
            _context = context;
            _serializer = serializer;
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
        [HttpGet("ContentByAuthor")]
        public async Task<IActionResult> GetBuilderContentByAuthor([FromQuery] Guid userId)
        {
            var content = await _context.BuilderContents
                                        .Where(x => x.AuthorID == userId)
                                        .ToListAsync();
            if (content.Count == 0)
                return NotFound("No content found.");

            return Ok(content);
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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(BuilderContent content)
        {
            return Ok();
        }
    }
}