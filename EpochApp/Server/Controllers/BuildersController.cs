// EpochWorlds
// BuildersController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpochApp.Server.Controllers
{
    /// <summary>
    ///    Controller for managing builders.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class BuildersController : ControllerBase
    {
        private readonly IBuilderService _builderService;

        /// <summary>
        ///  Constructor for the <see cref="BuildersController" />.
        /// </summary>
        /// <param name="builderService"> The <see cref="IBuilderService" />. </param>
        public BuildersController(IBuilderService builderService)
        {
            _builderService = builderService;
        }

        /// <summary>
        /// Retrieves the builder content for a specific content ID asynchronously.
        /// </summary>
        /// <param name="contentId">The ID of the builder content.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the <see cref="BuilderContentDTO"/> with the specified content ID.</returns>
        [HttpGet("Content/{contentId}")]
        public async Task<ActionResult<BuilderContentDTO>> GetBuilderContent(Guid contentId)
        {
            var content = await _builderService.GetBuilderContentByIdAsync(contentId);
            if (content == null)
                return NotFound("No content found.");

            return Ok(content);
        }

        /// <summary>
        /// Retrieves the builder content for a specific author asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the author.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of <see cref="BuilderContentDTO"/>.</returns>
        [HttpGet("ContentByAuthor/{userId}")]
        public async Task<ActionResult<IEnumerable<BuilderContentDTO>>> GetBuilderContentByAuthorAsync(Guid userId)
        {
            var content = await _builderService.GetBuilderContentByAuthorAsync(userId);
            return Ok(content);
        }

        /// <summary>
        /// Generates content for builder.
        /// </summary>
        /// <param name="contentId">The builder content id.</param>
        /// <param name="userId">The current user id.</param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="BuilderContentDTO"/>.</returns>
        [HttpGet("GeneratedContent/{contentId:guid}/{userId:guid}")]
        public async Task<IActionResult> GenerateContentAsync(Guid contentId, Guid userId)
        {
            if (contentId == Guid.Empty || userId == Guid.Empty)
                return BadRequest("Content id and User id cannot be empty");

            var result = await _builderService.GenerateContentAsync(contentId, userId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Creates a new builder content.
        /// </summary>
        /// <param name="content">The content to create.</param>
        /// <returns>A Task of BuilderContentDTO.</returns>
        [HttpPost]
        public async Task<ActionResult<BuilderContentDTO>> CreateNewBuilderContentAsync([FromBody] BuilderContentDTO content)
        {
            var newContent = await _builderService.CreateNewBuilderContentAsync(content);
            return Ok(newContent);
        }

        /// <summary>
        /// Updates the builder content.
        /// </summary>
        /// <param name="userId">The current user id.</param>
        /// <param name="contentId">The builder content id.</param>
        /// <param name="content">The content to update.</param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="BuilderContentDTO"/>.</returns>
        [HttpPut("{userId}/{contentId}")]
        public async Task<ActionResult<BuilderContentDTO>> UpdateBuilderAsync(Guid userId, Guid contentId, [FromBody] BuilderContentDTO content)
        {
            var updatedContent = await _builderService.UpdateBuilderAsync(userId, contentId, content);
            if (updatedContent == null)
                return NotFound();

            return Ok(updatedContent);
        }
    }
}