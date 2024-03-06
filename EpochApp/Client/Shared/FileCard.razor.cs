using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     A card to display a user file.
    /// </summary>
    public partial class FileCard
    {
        private bool _isEditMode = false;
        private UpdateFileDTO _updateFile = new UpdateFileDTO();

        /// <summary> The file to display. </summary>
        [Parameter] public UserFileDTO UserFile { get; set; }

        /// <summary>
        ///     Event to call when the file is removed or modified.
        /// </summary>
        [Parameter] public EventCallback<UserFileDTO> OnFileChanged { get; set; }

        [Inject] private HttpClient Client { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }

        [Inject] private IFileService FileService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }


        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (UserFile == null)
                return;

            _updateFile.FileId = UserFile.FileId;
            _updateFile.UserId = Auth.CurrentUser.UserID;
            _updateFile.Alias = UserFile.Alias;
            _updateFile.Alt = UserFile.AltText;
        }

        private async Task UpdateFileAsync()
        {
            _isEditMode = true;
            await FileService.UpdateFileInformationAsync(Auth.CurrentUser.UserID, _updateFile);
            await OnFileChanged.InvokeAsync(UserFile);
            _isEditMode = false;
        }

        private async Task OnRemoveFileAsync()
        {
            _isEditMode = false;
            var result = await DialogService.ShowMessageBox(
                         "Warning",
                         "Deleting can not be undone!",
                         "Delete!", cancelText: "Cancel");
            if (result != true)
                return;

            await FileService.RemoveFileAsync(Auth.CurrentUser.UserID, (int)UserFile.FileId);
            await OnFileChanged.InvokeAsync(UserFile);
            StateHasChanged();
        }
    }
}