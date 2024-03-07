// EpochWorlds
// BuilderContentFormBase.razor.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 6-3-2024
using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    ///    Base class for builder content forms.
    /// </summary>
    /// <typeparam name="TContent"> The content model. </typeparam>
    /// <typeparam name="UResult"> The result model. </typeparam>
    public class BuilderContentFormBase<TContent, UResult> : ComponentBase where TContent : class where UResult : class
    {
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
        [Parameter] public BuilderContentDTO BuilderContent { get; set; }

        /// <summary>
        ///     The active world relating to the builder form.
        /// </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

        /// <summary>
        ///     Injected <see cref="NavigationManager" />.
        /// </summary>
        [Inject] protected NavigationManager Nav { get; set; }

        /// <summary>
        ///     Injected <see cref="EpochAuthProvider" />.
        /// </summary>
        [Inject] protected EpochAuthProvider Auth { get; set; }

        /// <summary>
        ///    Injected <see cref="IBuilderService" />.
        /// </summary>
        [Inject] protected IBuilderService Builder { get; set; }

        /// <summary>
        ///     Injected <see cref="ILogger" />.
        /// </summary>
        [Inject] protected ILogger<BuilderFormBase<TContent, UResult>> Logger { get; set; }

        /// <summary>
        ///     Injected <see cref="ISerializationService" />.
        /// </summary>
        [Inject] protected ISerializationService Serializer { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            if (BuilderContent != null)
            {
                ContentModel = await Serializer.DeserializeFromXmlAsync<TContent>(BuilderContent.ContentXml);
                if (BuilderContent?.GeneratedXml != null)
                    ResultModel = await Serializer.DeserializeFromXmlAsync<UResult>(BuilderContent.GeneratedXml);
            }
            else
                ContentModel ??= Activator.CreateInstance<TContent>();

            await base.OnInitializedAsync();
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
                BuilderContent = new BuilderContentDTO() { ContentName = contentName };
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
            var genResponse = await Builder.GenerateContentAsync(BuilderContent.ContentID, Auth.CurrentUser.UserID);
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
            BuilderContent = new BuilderContentDTO()
                             {
                                 AuthorID = Auth.CurrentUser.UserID,
                                 WorldID = ActiveWorld.WorldId,
                                 ContentName = contentName,
                                 ContentType = type,
                                 DateCreated = DateTime.Now
                             };
            var contextXml = await Serializer.SerializeToXmlAsync(ContentModel) ?? "";
            BuilderContent.ContentXml = contextXml;

            var savedContent = await Builder.CreateNewBuilderContentAsync(BuilderContent);
            if (savedContent == null)
            {
                isLoading = false;
                isSavingOrUpdating = false;
                return;
            }

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
            var updatedContent = await Builder.UpdateBuilderAsync(Auth.CurrentUser.UserID, BuilderContent.ContentID, BuilderContent);
            if (updatedContent == null)
            {
                isLoading = false;
                isSavingOrUpdating = false;
                StateHasChanged();
                return;
            }
            BuilderContent = updatedContent;
            ContentModel = await Serializer.DeserializeFromXmlAsync<TContent>(BuilderContent.ContentXml);
            if (!string.IsNullOrEmpty(BuilderContent.GeneratedXml))
                ResultModel = await Serializer.DeserializeFromXmlAsync<UResult>(BuilderContent.GeneratedXml);
            isLoading = false;
            isSavingOrUpdating = false;
            StateHasChanged();
        }
    }
}