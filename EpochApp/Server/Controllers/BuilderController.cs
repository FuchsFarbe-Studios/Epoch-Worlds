// EpochWorlds
// BuilderController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 9-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreateNewContent(BuilderContent content)
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

        [Authorize(Roles = "ADMIN,INTERNAL")]
        [HttpPut("Content")]
        public async Task<IActionResult> UpdateContent([FromQuery] Guid userId, [FromQuery] Guid contentId, [FromBody] BuilderContent content)
        {
            // Verify the this is the sending users content to update
            var contentToUpdate = await _context.BuilderContents
                                                .FirstOrDefaultAsync(x => x.ContentID == contentId && x.AuthorID == userId);
            if (contentToUpdate == null)
                return NotFound("No content to update or you do not have permission to update this content.");

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
    }
}