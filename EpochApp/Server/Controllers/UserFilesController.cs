// EpochWorlds
// UserFilesController.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 26-2-2024
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Client;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserFilesController : ControllerBase
    {
        private readonly EpochDataDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<UserFilesController> _logger;
        public UserFilesController(EpochDataDbContext context, ILogger<UserFilesController> logger, IWebHostEnvironment environment)
        {
            _context = context;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet("UserFiles/{userId}")]
        public async Task<ActionResult<IEnumerable<UserFileDTO>>> GetUserFilesAsync(Guid userId)
        {
            var files = await _context.UserFiles.Where(x => x.UserId == userId && (x.RemovedOn == null || x.RemovedOn > DateTime.UtcNow))
                                      .Select(x => new UserFileDTO
                                                   {
                                                       FileId = x.FileId,
                                                       FilePath = x.FilePath,
                                                       AltText = x.ImageAlt,
                                                       // FileData = Convert.ToBase64String(x.)
                                                       SafeName = x.SafeName,
                                                       Alias = x.Alias,
                                                       UploadedOn = x.UploadedOn
                                                   })
                                      .ToListAsync();
            // foreach (var file in files)
            // {
            //     var path = Path.Combine(_environment.ContentRootPath, file.FilePath);
            //     var data = await System.IO.File.ReadAllBytesAsync(path);
            //     file.FileData = Convert.ToBase64String(data);
            // }
            return Ok(files);
        }

        [HttpGet("WorldFiles/{userId}/{worldId:guid}")]
        public async Task<ActionResult<IEnumerable<UserFileDTO>>> GetWorldFilesAsync(Guid userId, Guid worldId)
        {
            var files = await _context.UserFiles.Where(x => x.UserId == userId && x.WorldId == worldId)
                                      .Select(x => new UserFileDTO
                                                   {
                                                       FilePath = x.FilePath,
                                                       SafeName = x.SafeName,
                                                       Alias = x.Alias,
                                                       UploadedOn = x.UploadedOn
                                                   })
                                      .ToListAsync();
            return Ok(files);
        }

        // Update file data
        [HttpPut]
        public async Task<IActionResult> UpdateFileInformationAsync([FromQuery] Guid userId, [FromBody] UpdateFileDTO updateFile)
        {
            var file = await _context.UserFiles.FirstOrDefaultAsync(x => x.FileId == updateFile.FileId);
            if (file == null)
                return BadRequest("File not found.");
            if (file.UserId != userId)
                return BadRequest("You are not authorized to update this file.");

            file.Alias = updateFile.Alias;
            file.ImageAlt = updateFile.Alt;
            _context.UserFiles.Update(file);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Remove file
        [HttpDelete]
        public async Task<IActionResult> RemoveFileAsync([FromQuery] Guid userId, [FromQuery] int fileId)
        {
            var file = await _context.UserFiles.FirstOrDefaultAsync(x => x.FileId == fileId);
            if (file == null)
                return BadRequest("File not found.");
            if (file.UserId != userId)
                return BadRequest("You are not authorized to remove this file.");

            file.RemovedOn = DateTime.UtcNow;
            _context.UserFiles.Update(file);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("UserFile")]
        public async Task<IActionResult> UploadUserFileAsync([FromQuery] Guid userId, [FromBody] FileUploadDto fileUploadDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null)
                return BadRequest("Not a valid user.");
            if (userId != user.UserID)
                return BadRequest("You are not authorized to upload files for this user.");
            // if (fileUploadDto.File.Size == 0)
            //     return BadRequest("No file uploaded.");

            var worldDirPath = Path.Combine(StaticUtils.Constants.UserFilesDirectory, user.UserName);// Adjust this path accordingly
            if (!Directory.Exists(worldDirPath))
                Directory.CreateDirectory(worldDirPath);

            var extension = Path.GetExtension(fileUploadDto?.FileName);
            var uniqueFileName = Path.GetRandomFileName();
            uniqueFileName = uniqueFileName.Split(".").FirstOrDefault();
            uniqueFileName += extension;
            var filePath = Path.Combine(worldDirPath, uniqueFileName);

            try
            {
                _logger.LogWarning($"Uploading file: {fileUploadDto?.FileName} to {worldDirPath} as {uniqueFileName}");
                // Write file data to the disk
                var data = Convert.FromBase64String(fileUploadDto.FileData);
                await System.IO.File.WriteAllBytesAsync(filePath, data);
                var fileData = new UserFile
                               {
                                   UserId = user.UserID,
                                   WorldId = fileUploadDto.WorldId,
                                   FileName = fileUploadDto.FileName,
                                   SafeName = uniqueFileName,
                                   ImageAlt = fileUploadDto?.Alt ?? null,
                                   Alias = fileUploadDto.Alias ?? null,
                                   FilePath = filePath.Replace("\\", "/"),
                                   FileSize = fileUploadDto.FileSize,
                                   ContentType = extension,
                                   UploadedOn = DateTime.UtcNow
                               };
                _context.UserFiles.Add(fileData);
                await _context.SaveChangesAsync();
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

            return Ok(new { FilePath = filePath });
        }

        [HttpPost("WorldFile")]
        public async Task<IActionResult> UploadWorldFileAsync([FromQuery] Guid userId, [FromQuery] Guid worldId, [FromBody] FileUploadDto fileUploadDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            var world = await _context.Worlds.FirstOrDefaultAsync(x => x.WorldID == worldId);
            if (user == null || world == null)
                return BadRequest("Not a valid user or world.");
            if (userId != user.UserID || world.OwnerID != user.UserID)
                return BadRequest("You are not authorized to upload files for this user.");
            if (userId != world.OwnerID)
                return BadRequest("You are not authorized to upload files for this world.");
            // if (fileUploadDto.File.Size == 0)
            //     return BadRequest("No file uploaded.");

            var userDirPath = Path.Combine(StaticUtils.Constants.WorldFilesDirectory, world.WorldName);// Adjust this path accordingly
            if (!Directory.Exists(userDirPath))
                Directory.CreateDirectory(userDirPath);

            var extension = Path.GetExtension(fileUploadDto.FileName);
            var uniqueFileName = Path.GetRandomFileName();
            uniqueFileName = uniqueFileName.Split(".").FirstOrDefault();
            uniqueFileName += extension;
            var filePath = Path.Combine(userDirPath, uniqueFileName);

            try
            {
                _logger.LogWarning($"Uploading file: {fileUploadDto.FileName} to {userDirPath} as {uniqueFileName}");
                // Write file data to the disk
                var data = Convert.FromBase64String(fileUploadDto.FileData);
                await System.IO.File.WriteAllBytesAsync(filePath, data);
                var fileData = new UserFile
                               {
                                   UserId = user.UserID,
                                   WorldId = fileUploadDto.WorldId,
                                   FileName = fileUploadDto.FileName,
                                   SafeName = uniqueFileName,
                                   Alias = fileUploadDto.Alias ?? null,
                                   ImageAlt = fileUploadDto?.Alt ?? null,
                                   FilePath = filePath.Replace("\\", "/"),
                                   FileSize = fileUploadDto.FileSize,
                                   ContentType = extension,
                                   UploadedOn = DateTime.UtcNow
                               };
                _context.UserFiles.Add(fileData);
                await _context.SaveChangesAsync();
            }
            catch
            {
                _logger.LogError("Error uploading file!"
                                 + "\n\tFile: "
                                 + fileUploadDto?.FileName);
                return BadRequest("Something went wrong!");
            }

            return Ok(new { FilePath = filePath });
        }
    }
}