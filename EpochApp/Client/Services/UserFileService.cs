// EpochWorlds
// UserFileService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using EpochApp.Shared;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    #pragma warning disable CS1591
    public class UserFileService : IFileService
    {
        private readonly HttpClient _client;
        private readonly ILogger<IFileService> _logger;
        private readonly ISnackbar _snackbar;

        public UserFileService(HttpClient client, ILogger<UserFileService> logger, ISnackbar snackbar)
        {
            _client = client;
            _logger = logger;
            _snackbar = snackbar;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserFileDTO>> GetUserFilesAsync(Guid userId)
        {
            var files = await _client.GetFromJsonAsync<List<UserFileDTO>>($"api/v1/UserFiles/UserFiles/{userId}");
            return files;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserFileDTO>> GetUserFilesAsync(Guid userId, Guid worldId)
        {
            var files = await _client.GetFromJsonAsync<List<UserFileDTO>>($"api/v1/UserFiles/WorldFiles/{userId}/{worldId}");
            return files;
        }

        /// <inheritdoc />
        public async Task UpdateFileInformationAsync(Guid userId, UpdateFileDTO updateFile)
        {
            _snackbar.Add("Updating file...", Severity.Info);
            var response = await _client.PutAsJsonAsync($"/api/v1/UserFiles?userId={userId}", updateFile);
            if (response.IsSuccessStatusCode)
                _snackbar.Add("File updated.", Severity.Success);
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _snackbar.Add(message, Severity.Error);
            }
        }

        /// <inheritdoc />
        public async Task RemoveFileAsync(Guid userId, int fileId)
        {
            _snackbar.Add("Deleting file...", Severity.Info);
            var response = await _client.DeleteAsync($"/api/v1/UserFiles?userId={userId}&fileId={fileId}");
            if (response.IsSuccessStatusCode)
                _snackbar.Add("File deleted.", Severity.Success);
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _snackbar.Add(message, Severity.Error);
            }
        }

        /// <inheritdoc />
        public async Task UploadFileAsync(Guid userId, FileUploadDTO fileUploadDto)
        {
            _snackbar.Add("Uploading file...", Severity.Info);
            var response = await _client.PostAsJsonAsync($"/api/v1/UserFiles/UserFile?userId={userId}", fileUploadDto);
            if (response.IsSuccessStatusCode)
                _snackbar.Add("File uploaded.", Severity.Success);
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _snackbar.Add(message, Severity.Error);
            }
        }

        /// <inheritdoc />
        public async Task UploadFileAsync(Guid userId, Guid worldId, FileUploadDTO fileUploadDto)
        {
            _snackbar.Add("Uploading file...", Severity.Info);
            var response = await _client.PostAsJsonAsync($"/api/v1/UserFiles/WorldFile?userId={userId}&worldId={worldId}", fileUploadDto);
            if (response.IsSuccessStatusCode)
                _snackbar.Add("File uploaded.", Severity.Success);
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                _snackbar.Add(message, Severity.Error);
            }
        }
    }
}