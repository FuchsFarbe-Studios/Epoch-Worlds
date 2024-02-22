using EpochApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     A selector for user content.
    /// </summary>
    public partial class ContentSelector
    {
        private List<BuilderContent> _userContent = new List<BuilderContent>();
        private BuilderContent _selectedContent;

        /// <summary>
        ///     The type of user content to filter for.
        /// </summary>
        [Parameter] public ContentType? Type { get; set; }

        /// <summary>
        ///     The event that is called when the selected content is changed.
        /// </summary>
        [Parameter] public EventCallback<BuilderContent> OnContentChanged { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Auth?.CurrentUser?.UserID != Guid.Empty && Type != null)
            {
                var contents = await Client.GetFromJsonAsync<List<BuilderContent>>($"api/v1/Builder/ContentByAuthor/{Auth.CurrentUser.UserID}");
                if (contents.Count > 0)
                    _userContent = contents.Where(x => x.ContentType == Type).ToList();
            }
            _selectedContent = _userContent.FirstOrDefault();
            await ContentChanged(_selectedContent);
        }
        private async Task ContentChanged(BuilderContent e)
        {
            _selectedContent = e;
            await OnContentChanged.InvokeAsync(_selectedContent);
        }
    }
}