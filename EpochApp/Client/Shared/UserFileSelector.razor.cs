using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     A selector for user files.
    /// </summary>
    public partial class UserFileSelector
    {
        private UserFileDTO _selectedFile;
        private List<UserFileDTO> _userFiles = new List<UserFileDTO>();
        /// <summary>
        ///     The event that is called when the selected content is changed.
        /// </summary>
        [Parameter] public EventCallback<UserFileDTO> OnFileSelectionChanged { get; set; }

        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private IFileService Client { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
            {
                var contents = await Client.GetUserFilesAsync(Auth.CurrentUser.UserID);
                if (contents.Any())
                    _userFiles = contents?.ToList();
            }
            _selectedFile = _userFiles.FirstOrDefault();
            await base.OnInitializedAsync();
        }

        private async Task ContentChanged(UserFileDTO e)
        {
            _selectedFile = e;
            await OnFileSelectionChanged.InvokeAsync(_selectedFile);
        }
    }
}