using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

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

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Auth?.CurrentUser?.UserID != Guid.Empty)
            {
                var contents = await Client.GetFromJsonAsync<List<UserFileDTO>>($"api/v1/UserFiles/UserFiles/{Auth.CurrentUser.UserID}");
                if (contents.Any())
                    contents.ForEach(x => _userFiles.Add(x));
            }
            // _selectedFile = _userFiles.FirstOrDefault();
            // await ContentChanged(_selectedFile);
        }

        private async Task ContentChanged(UserFileDTO e)
        {
            _selectedFile = e;
            await OnFileSelectionChanged.InvokeAsync(_selectedFile);
        }
    }
}