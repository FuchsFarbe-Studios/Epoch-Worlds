// EpochWorlds
// UserFilesController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 1591// Missing XML comment for publicly visible type or member
namespace EpochApp.Server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserFilesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly IFileService _fileService;
        private readonly ILogger<UserFilesController> _logger;

        public UserFilesController(EpochDataDbContext context, ILogger<UserFilesController> logger, IFileService fileService)
        {
            _context = context;
            _logger = logger;
            _fileService = fileService;
        }

        /// <summary>
        /// Get all files for a user.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="ActionResult{T}"/> of <see cref="IEnumerable{T}"/> of <see cref="UserFileDTO"/>. </returns>
        [HttpGet("UserFiles/{userId}")]
        public async Task<ActionResult<IEnumerable<UserFileDTO>>> GetUserFilesAsync(Guid userId)
        {
            var files = await _fileService.GetUserFilesAsync(userId);
            return Ok(files);
        }

        /// <summary>
        ///  Get all files for a world.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <param name="worldId"> The world's ID. </param>
        /// <returns>   <see cref="Task{TResult}"/> of <see cref="ActionResult{T}"/> of <see cref="IEnumerable{T}"/> of <see cref="UserFileDTO"/>. </returns>
        [HttpGet("WorldFiles/{userId:guid}/{worldId:guid}")]
        public async Task<ActionResult<IEnumerable<UserFileDTO>>> GetWorldFilesAsync(Guid userId, Guid worldId)
        {
            var files = await _fileService.GetUserFilesAsync(userId, worldId);
            return Ok(files);
        }

        // Update file data
        [HttpPut]
        public async Task<IActionResult> UpdateFileInformationAsync([FromQuery] Guid userId, [FromBody] UpdateFileDTO updateFile)
        {
            if (updateFile == null)
                return BadRequest("File not found.");
            if (updateFile.UserId != userId)
                return BadRequest("You are not authorized to update this file.");

            try
            {
                await _fileService.UpdateFileInformationAsync(userId, updateFile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // Remove file
        [HttpDelete]
        public async Task<IActionResult> RemoveFileAsync([FromQuery] Guid userId, [FromQuery] int fileId)
        {
            try
            {
                await _fileService.RemoveFileAsync(userId, fileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("UserFile")]
        public async Task<IActionResult> UploadUserFileAsync([FromQuery] Guid userId, [FromBody] FileUploadDTO fileUploadDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
                return BadRequest("Not a valid user.");
            if (userId != user.UserID)
                return BadRequest("You are not authorized to upload files for this user.");

            try
            {
                await _fileService.UploadFileAsync(userId, fileUploadDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error uploading file!"
                                 + "\n\tFile: "
                                 + fileUploadDto?.FileName
                                 + $"\n\tException: {ex.Message}"
                                 + $"\n\tStackTrace: {ex.StackTrace}");
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost("WorldFile")]
        public async Task<IActionResult> UploadWorldFileAsync([FromQuery] Guid userId, [FromQuery] Guid worldId, [FromBody] FileUploadDTO fileUploadDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            var world = await _context.Worlds.FirstOrDefaultAsync(x => x.WorldId == worldId);
            if (user == null || world == null)
                return BadRequest("Not a valid user or world.");
            if (userId != user.UserID || world.OwnerId != user.UserID)
                return BadRequest("You are not authorized to upload files for this user.");
            if (userId != world.OwnerId)
                return BadRequest("You are not authorized to upload files for this world.");

            try
            {
                await _fileService.UploadFileAsync(userId, worldId, fileUploadDto);
                return Ok();
            }
            catch
            {
                _logger.LogError("Error uploading file!"
                                 + "\n\tFile: "
                                 + fileUploadDto?.FileName);
                return BadRequest("Something went wrong!");
            }
        }
    }
}