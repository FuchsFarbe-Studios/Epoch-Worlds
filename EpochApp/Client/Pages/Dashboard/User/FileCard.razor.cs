using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.User
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
            var response = await Client.PutAsJsonAsync<UpdateFileDTO>($"/api/v1/UserFiles?userId={Auth.CurrentUser.UserID}", _updateFile);
            if (response.IsSuccessStatusCode)
            {
                await OnFileChanged.InvokeAsync(UserFile);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                await DialogService.ShowMessageBox("Error", message);
            }
            _isEditMode = false;
            //await OnFileChanged.InvokeAsync(UserFile);
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

            var response = await Client.DeleteAsync($"/api/v1/UserFiles?userId={Auth.CurrentUser.UserID}&fileId={UserFile.FileId}");
            if (response.IsSuccessStatusCode)
            {
                await OnFileChanged.InvokeAsync(UserFile);
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                await DialogService.ShowMessageBox("Error", message);
            }
            StateHasChanged();
            await OnFileChanged.InvokeAsync(UserFile);
        }
    }
}