using EpochApp.Client.Services;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Pages.Dashboard.Worlds
{
    /// <summary>
    /// The world files component.
    /// </summary>
    public partial class WorldFiles
    {
        private List<UserFileDTO> _files = new List<UserFileDTO>();
        /// <summary>
        /// The active world.
        /// </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private IFileService FileService { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            var files = await FileService.GetUserFilesAsync(Auth.CurrentUser.UserID, ActiveWorld.WorldId);
            if (files.Any())
                _files = files.ToList();
            await base.OnInitializedAsync();
        }
    }
}