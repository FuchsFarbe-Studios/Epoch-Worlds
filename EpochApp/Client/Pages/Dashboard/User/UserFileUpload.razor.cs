using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.User
{
    /// <summary>
    ///     User file upload component.
    /// </summary>
    public partial class UserFileUpload
    {
        private IList<IBrowserFile> _files = new List<IBrowserFile>();
        private List<string> _messages = new List<string>();
        private int _percentDone = 0;
        private List<FileUploadDto> _uploadedFiles = new List<FileUploadDto>();
        private bool _uploading = false;

        /// <summary>
        ///     Whether the file is a world file.
        /// </summary>
        [Parameter] public bool IsWorldFile { get; set; }

        [CascadingParameter] private UserWorldDTO ActiveWorld { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private HttpClient Client { get; set; }

        [Inject] private ILogger<UserFileUpload> Logger { get; set; }

        private async Task UpdateUploadedFiles(IBrowserFile file)
        {
            _uploading = true;
            var buffer = new byte[file.Size];
            var readData = await file.OpenReadStream().ReadAsync(buffer);
            var data = Convert.ToBase64String(buffer);
            var newFile = new FileUploadDto
                          {
                              FileName = file.Name,
                              FileSize = file.Size,
                              FileData = data,
                              Alias = null,
                              UserId = Auth.CurrentUser.UserID,
                              WorldId = null
                          };
            if (IsWorldFile)
                newFile.WorldId = ActiveWorld?.WorldId;
            _uploadedFiles.Add(newFile);
            _uploading = false;
            StateHasChanged();
        }

        private async Task OnFileRemovedAsync(FileUploadDto file)
        {
            Logger.LogInformation($"Removing file {file.FileName}");
            if (_uploadedFiles.Contains(file))
                _uploadedFiles.Remove(file);
            StateHasChanged();
            await Task.CompletedTask;
        }

        private async Task OnUploadFiles()
        {
            _uploading = true;
            if (!_uploadedFiles.Any())
            {
                _uploading = false;
                return;
            }

            var processedFiles = 0;
            try
            {
                foreach (var file in _uploadedFiles)
                {
                    Logger.LogInformation($"Uploading file {file.FileName}");
                    if (IsWorldFile)
                    {
                        var response = await Client.PostAsJsonAsync($"api/v1/UserFiles/WorldFile?userId={Auth.CurrentUser.UserID}&worldId={ActiveWorld.WorldId}", file);
                        if (!response.IsSuccessStatusCode)
                        {
                            var msg = await response.Content.ReadAsStringAsync();
                            Logger.LogError($"Error uploading file {file.FileName}: {msg}");
                            _messages.Add(msg);
                        }
                        else
                        {
                            _messages.Add($"File {file.FileName} uploaded successfully");
                            Logger.LogInformation($"File {file.FileName} uploaded successfully");
                        }
                    }
                    else
                    {
                        var response = await Client.PostAsJsonAsync($"api/v1/UserFiles/UserFile?userId={Auth.CurrentUser.UserID}", file);
                        if (!response.IsSuccessStatusCode)
                        {
                            var msg = await response.Content.ReadAsStringAsync();
                            Logger.LogError($"Error uploading file {file.FileName}: {msg}");
                            _messages.Add(msg);
                        }
                        else
                        {
                            _messages.Add($"File {file.FileName} uploaded successfully");
                            Logger.LogInformation($"File {file.FileName} uploaded successfully");
                        }
                    }
                    processedFiles += 1;
                    _percentDone = processedFiles / _uploadedFiles.Count;
                    StateHasChanged();
                    await Task.Delay(200);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error uploading files");
                _uploading = false;
                return;
            }

            _uploading = false;
            await Task.CompletedTask;
        }
    }
}