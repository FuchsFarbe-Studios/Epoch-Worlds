using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    ///     Base class for builder forms.
    /// </summary>
    public class BuilderFormBase<TContent, UResult> : ComponentBase where TContent : class where UResult : class
    {
        /// <summary>
        ///     Errors returned from the server's ModelState.
        /// </summary>
        protected Dictionary<string, List<string>> errorDict = new Dictionary<string, List<string>>();

        /// <summary>
        ///     Whether the form is currently loading or generating.
        /// </summary>
        protected bool isLoading;

        /// <summary>
        ///    Whether the form is currently saving or updating.
        /// </summary>
        protected bool isSavingOrUpdating;

        /// <summary>
        ///     Used to toggle results panels.
        /// </summary>
        protected bool showResults = false;

        /// <summary>
        ///     Whether the form is in edit mode or create mode.
        /// </summary>
        [Parameter] public bool IsEditMode { get; set; }

        /// <summary>
        ///     The content to hold the generation options and to act as the form model.
        /// </summary>
        [Parameter] public TContent ContentModel { get; set; }

        /// <summary>
        ///     The generated result content.
        /// </summary>
        [Parameter] public UResult ResultModel { get; set; }

        /// <summary>
        ///     The content of the form.
        /// </summary>
        [Parameter] public BuilderContent BuilderContent { get; set; }

        /// <summary>
        ///     The active world relating to the builder form.
        /// </summary>
        [CascadingParameter] protected UserWorldDTO ActiveWorld { get; set; }

        /// <summary>
        ///     Injected <see cref="HttpClient" />.
        /// </summary>
        [Inject] protected HttpClient Client { get; set; }

        /// <summary>
        ///     Injected <see cref="NavigationManager" />.
        /// </summary>
        [Inject] protected NavigationManager Nav { get; set; }

        /// <summary>
        ///     Injected <see cref="ILogger" />.
        /// </summary>
        [Inject] protected ILogger<BuilderFormBase<TContent, UResult>> Logger { get; set; }

        /// <summary>
        ///     Injected <see cref="EpochAuthProvider" />.
        /// </summary>
        [Inject] protected EpochAuthProvider Auth { get; set; }

        /// <summary>
        ///     Injected <see cref="ISerializationService" />.
        /// </summary>
        [Inject] protected ISerializationService Serializer { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (BuilderContent != null)
            {
                ContentModel = await Serializer.DeserializeFromXmlAsync<TContent>(BuilderContent.ContentXml);
                if (BuilderContent?.GeneratedXml != null)
                {
                    ResultModel = await Serializer.DeserializeFromXmlAsync<UResult>(BuilderContent.GeneratedXml);
                }
            }
            else
            {
                ContentModel ??= Activator.CreateInstance<TContent>();
            }
        }

        /// <summary>
        ///     Set the content name for the builder.
        /// </summary>
        /// <param name="contentName">
        ///     The name of the content.
        /// </param>
        /// <returns> A <see cref="Task" />. </returns>
        protected Task SetBuilderContentNameAsync(string contentName)
        {
            if (BuilderContent == null)
                BuilderContent = new BuilderContent
                                 {
                                     ContentName = contentName
                                 };
            else
                BuilderContent.ContentName = contentName;
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Generate data from the form.
        /// </summary>
        protected virtual async Task GenerateContentAsync()
        {
            isLoading = true;
            Logger.LogInformation("Generating...");
            await UpdateExistingBuilderContentAsync();
            var genResponse = await Client.GetFromJsonAsync<BuilderContent>($"api/v1/Builder/GeneratedContent?contentId={BuilderContent.ContentID}&userId={Auth.CurrentUser.UserID}");
            if (genResponse == null)
            {
                Logger.LogWarning($"Retrieving generated content failed...\n\tBuilder: {BuilderContent.ContentID}\n\tUser: {Auth.CurrentUser.UserID}");
                ResultModel = null;
                isLoading = false;
                StateHasChanged();
                return;
            }

            BuilderContent = genResponse;
            ContentModel = await Serializer.DeserializeFromXmlAsync<TContent>(BuilderContent.ContentXml);
            ResultModel = await Serializer.DeserializeFromXmlAsync<UResult>(BuilderContent.GeneratedXml);
            isLoading = false;
            StateHasChanged();
        }

        /// <summary>
        ///     Save new builder content.
        /// </summary>
        /// <param name="contentName">
        ///     The name of the new content.
        /// </param>
        /// <param name="type">
        ///     The type of the new content.
        /// </param>
        protected async Task SaveNewBuilderContentAsync(string contentName, ContentType type)
        {
            isSavingOrUpdating = true;
            Logger.LogInformation("Saving...");
            BuilderContent = new BuilderContent
                             {
                                 AuthorID = Auth.CurrentUser.UserID,
                                 WorldID = ActiveWorld.WorldId,
                                 ContentName = contentName,
                                 ContentType = type,
                                 DateCreated = DateTime.Now
                             };
            var contextXml = await Serializer.SerializeToXmlAsync(ContentModel) ?? "";
            BuilderContent.ContentXml = contextXml;

            var saveResponse = await Client.PostAsJsonAsync<BuilderContent>("api/v1/Builder/Content", BuilderContent);
            if (!saveResponse.IsSuccessStatusCode)
            {
                isLoading = false;
                isSavingOrUpdating = false;
                return;
            }

            var savedContent = await saveResponse.Content.ReadFromJsonAsync<BuilderContent>();
            BuilderContent = savedContent;
            var savedContentData = await Serializer.DeserializeFromXmlAsync<TContent>(BuilderContent.ContentXml);
            ContentModel = savedContentData;
            isLoading = false;
            isSavingOrUpdating = false;
            StateHasChanged();
        }

        /// <summary>
        ///     Update existing builder content.
        /// </summary>
        protected virtual async Task UpdateExistingBuilderContentAsync()
        {
            isLoading = true;
            isSavingOrUpdating = true;
            Logger.LogInformation("Updating...");
            BuilderContent.ContentXml = await Serializer.SerializeToXmlAsync(ContentModel) ?? "";
            var updateResponse = await Client.PutAsJsonAsync<BuilderContent>($"api/v1/Builder/Content?userId={Auth.CurrentUser.UserID}&contentId={BuilderContent.ContentID}", BuilderContent);
            if (!updateResponse.IsSuccessStatusCode)
            {
                isLoading = false;
                isSavingOrUpdating = false;
                var error = await updateResponse.Content.ReadAsStringAsync();
                Logger.LogWarning("Failed up update content!\n\t" + error);
                StateHasChanged();
                return;
            }

            var updatedContent = await updateResponse.Content.ReadFromJsonAsync<BuilderContent>();
            BuilderContent = updatedContent;
            var updatedContentData = await Serializer.DeserializeFromXmlAsync<TContent>(BuilderContent.ContentXml);
            ContentModel = updatedContentData;
            if (!string.IsNullOrEmpty(BuilderContent.GeneratedXml))
            {
                var genData = await Serializer.DeserializeFromXmlAsync<UResult>(BuilderContent.GeneratedXml);
                ResultModel = genData;
            }
            isLoading = false;
            isSavingOrUpdating = false;
            StateHasChanged();
        }
    }
}