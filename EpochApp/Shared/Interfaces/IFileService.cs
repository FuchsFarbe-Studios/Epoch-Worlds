// EpochWorlds
// IFileService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    /// Interface for the file service.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Get the files for a user.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="IEnumerable{T}"/> of <see cref="UserFileDTO"/>. </returns>
        Task<IEnumerable<UserFileDTO>> GetUserFilesAsync(Guid userId);

        /// <summary>
        /// Get the files for a user.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <param name="worldId"> The world's ID. </param>
        /// <returns> <see cref="Task{TResult}"/> of <see cref="IEnumerable{T}"/> of <see cref="UserFileDTO"/>. </returns>
        Task<IEnumerable<UserFileDTO>> GetUserFilesAsync(Guid userId, Guid worldId);

        /// <summary>
        /// Update the file information.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <param name="updateFile"> The file information to update. </param>
        /// <returns> <see cref="Task"/>. </returns>
        Task UpdateFileInformationAsync(Guid userId, UpdateFileDTO updateFile);

        /// <summary>
        /// Remove a file.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <param name="fileId"> The file's ID. </param>
        /// <returns> <see cref="Task"/>. </returns>
        Task RemoveFileAsync(Guid userId, int fileId);

        /// <summary>
        /// Upload a file.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <param name="fileUploadDto"> The file upload data. </param>
        /// <returns> <see cref="Task"/>. </returns>
        Task UploadFileAsync(Guid userId, FileUploadDTO fileUploadDto);

        /// <summary>
        /// Upload a file.
        /// </summary>
        /// <param name="userId"> The user's ID. </param>
        /// <param name="worldId"> The world's ID. </param>
        /// <param name="fileUploadDto"> The file upload data. </param>
        /// <returns> <see cref="Task"/>. </returns>
        Task UploadFileAsync(Guid userId, Guid worldId, FileUploadDTO fileUploadDto);
    }
}