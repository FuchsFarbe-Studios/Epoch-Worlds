using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     Base class for components that need to make requests to the server.
    /// </summary>
    /// <typeparam name="TModel">
    ///     The type of model to request from the server.
    /// </typeparam>
    public class RequestComponent<TModel> : ComponentBase where TModel : class
    {
        /// <summary>
        ///     The logger for this component.
        /// </summary>
        [Inject] protected ILogger<RequestComponent<TModel>> Logger { get; set; }

        /// <summary>
        ///     The authentication provider for this component.
        /// </summary>
        [Inject] protected EpochAuthProvider Auth { get; set; }

        /// <summary>
        ///     The navigation manager for this component.
        /// </summary>
        [Inject] protected NavigationManager Nav { get; set; }

        /// <summary>
        ///     The HTTP client for this component.
        /// </summary>
        [Inject] protected HttpClient Client { get; set; }
    }
}