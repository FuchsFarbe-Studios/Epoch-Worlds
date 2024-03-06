// EpochWorlds
// UserFileService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using AutoMapper;
using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace EpochApp.Server.Services
{
    #pragma warning disable CS1591
    public class UserFileService : IFileService
    {
        private readonly EpochDataDbContext _context;
        private readonly ILogger<IFileService> _logger;
        private readonly IMapper _mapper;

        public UserFileService(ILogger<UserFileService> logger, IMapper mapper, EpochDataDbContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<UserFileDTO>> GetUserFilesAsync(Guid userId)
        {
            var userFiles = await _context.Users.Where(x => x.UserID == userId)
                                          .Include(x => x.UserFiles)
                                          .Select(x => x.UserFiles)
                                          .AsSplitQuery()
                                          .FirstOrDefaultAsync();
            var files = userFiles.Select(x => _mapper.Map<UserFileDTO>(x));
            return files;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserFileDTO>> GetUserFilesAsync(Guid userId, Guid worldId)
        {
            var userFiles = await _context.Worlds.Where(x => x.OwnerId == userId && x.WorldId == worldId)
                                          .Include(x => x.WorldFiles)
                                          .Select(x => x.WorldFiles)
                                          .AsSplitQuery()
                                          .FirstOrDefaultAsync();
            var files = userFiles.Select(x => _mapper.Map<UserFileDTO>(x));
            return files;
        }

        /// <inheritdoc />
        public async Task UpdateFileInformationAsync(Guid userId, UpdateFileDTO updateFile)
        {
            var file = await _context.UserFiles.FirstOrDefaultAsync(x => x.FileId == updateFile.FileId);
            file.Alias = updateFile.Alias;
            file.ImageAlt = updateFile.Alt;
            _context.UserFiles.Update(file);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task RemoveFileAsync(Guid userId, int fileId)
        {
            var file = await _context.UserFiles.FirstOrDefaultAsync(x => x.FileId == fileId);
            if (file == null)
                return;

            file.RemovedOn = DateTime.UtcNow;
            _context.UserFiles.Update(file);
            await _context.SaveChangesAsync();
        }

        public async Task UploadFileAsync(Guid userId, FileUploadDTO fileUploadDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
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
                throw;
            }
        }

        public async Task UploadFileAsync(Guid userId, Guid worldId, FileUploadDTO fileUploadDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserID == userId);
            var world = await _context.Worlds.FirstOrDefaultAsync(x => x.WorldId == worldId);
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
                throw;
            }
        }
    }
}