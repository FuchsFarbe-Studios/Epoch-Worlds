using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Pages.Dashboard.Builders
{
    public partial class ConLangEdit
    {
        private ConstructedLanguage LangContent { get; set; } = null!;
        /// <summary>
        ///     Content Id of the Constructed Language to edit.
        /// </summary>
        [Parameter] public string ContentId { get; set; }
        [Inject] private HttpClient Client { get; set; }
        [Inject] private ISerializationService Serializer { get; set; }
        [Inject] private NavigationManager Nav { get; set; }
        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (Guid.TryParse(ContentId, out var contentId))
            {
                var content = await Client.GetFromJsonAsync<BuilderContent>($"api/v1/Builder/Content?contentId={contentId}");
                if (content != null)
                {
                    var langContent = await Serializer.DeserializeFromXml<ConstructedLanguage>(content.ContentXml);
                    if (langContent != null)
                        LangContent = langContent;
                }
            }
        }
    }
}