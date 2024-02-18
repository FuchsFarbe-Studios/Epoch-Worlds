using EpochApp.Client.Services;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared.Forms
{
    /// <summary>
    ///     Base class for builder forms.
    /// </summary>
    public class BuilderFormBase : ComponentBase
    {
        /// <summary>
        ///     Whether the form is currently loading.
        /// </summary>
        protected bool _isLoading = false;

        /// <summary>
        ///     Errors returned from the server's ModelState.
        /// </summary>
        protected Dictionary<string, List<string>> errorDict = new Dictionary<string, List<string>>();

        /// <summary>
        ///     The content of the form.
        /// </summary>
        protected BuilderContent Content { get; set; }

        /// <summary>
        ///     The active world relating to the builder form.
        /// </summary>
        [CascadingParameter] protected WorldDTO ActiveWorld { get; set; }

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
        [Inject] protected ILogger<BuilderFormBase> Logger { get; set; }

        /// <summary>
        ///     Injected <see cref="EpochAuthProvider" />.
        /// </summary>
        [Inject] protected EpochAuthProvider Auth { get; set; }

        /// <summary>
        ///     Injected <see cref="ISerializationService" />.
        /// </summary>
        [Inject] protected ISerializationService Serializer { get; set; }

        /// <summary>
        ///     Generate data from the form.
        /// </summary>
        protected virtual async Task GenerateAsync()
        {
            Logger.LogInformation("Generating...");
            await Task.CompletedTask;
        }
    }
}