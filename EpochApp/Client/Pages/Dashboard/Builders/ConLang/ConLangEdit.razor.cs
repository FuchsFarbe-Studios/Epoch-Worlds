using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    /// <summary>
    ///     The edit page for a constructed language.
    /// </summary>
    public partial class ConLangEdit
    {
        private BuilderContent _editContent = null!;
        private ConstructedLanguage LangContent { get; set; } = null!;
        /// <summary>
        ///     Content Id of the Constructed Language to edit.
        /// </summary>
        [Parameter] public string ContentId { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private ISerializationService Serializer { get; set; }
        [Inject] private EpochAuthProvider Auth { get; set; }
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] private ILogger<ConLangEdit> Logger { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (!string.IsNullOrWhiteSpace(ContentId) && Guid.TryParse(ContentId, out var contentId))
            {
                await Task.Delay(200);
                var content = await Client.GetFromJsonAsync<BuilderContent>($"api/v1/Builder/Content?contentId={contentId}");
                if (content != null)
                {
                    _editContent = content;
                    var langContent = await Serializer.DeserializeFromXmlAsync<ConstructedLanguage>(_editContent.ContentXml);
                    if (langContent != null)
                        LangContent = langContent;
                }
            }
        }

        /// <inheritdoc />
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            Logger.LogInformation("Parameters changed\n\tContentId: {ContentId}", ContentId);
            if (!string.IsNullOrWhiteSpace(ContentId) && Guid.TryParse(ContentId, out var contentId))
            {
                await Task.Delay(200);
                var content = await Client.GetFromJsonAsync<BuilderContent>($"api/v1/Builder/Content?contentId={contentId}");
                if (content != null)
                {
                    _editContent = content;
                    var langContent = await Serializer.DeserializeFromXmlAsync<ConstructedLanguage>(_editContent.ContentXml);
                    if (langContent != null)
                        LangContent = langContent;
                    Logger.LogInformation("Changing state");
                    StateHasChanged();
                }
            }
        }

        private async Task HandleContentChanged(BuilderContent arg)
        {
            _editContent = arg;
            await Task.CompletedTask;
        }
    }
}