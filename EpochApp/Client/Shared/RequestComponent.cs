using EpochApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace EpochApp.Client.Shared
{
    /// <summary>
    ///     Base class for components that need to make requests to the server.
    /// </summary>
    /// <typeparam name="TModel">
    ///     The type of model to request from the server.
    /// </typeparam>
    public class RequestComponent<TModel> : ComponentBase, IDisposable where TModel : new()
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

        /// <summary>
        ///     User service for receiving tokens and setting headers.
        /// </summary>
        [Inject] protected EpochUserService UserService { get; set; }

        /// <summary>
        ///     The model for this component.
        /// </summary>
        protected virtual TModel Model { get; set; } = new TModel();

        /// <inheritdoc />
        public void Dispose()
        {
            Auth.AuthenticationStateChanged -= RefreshHeaders;
        }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            Auth.AuthenticationStateChanged += RefreshHeaders;
            base.OnInitialized();
        }

        /// <summary>
        ///     Sets the authorization header for the user making the request.
        /// </summary>
        /// <param name="task"> </param>
        private void RefreshHeaders(Task<AuthenticationState> task)
        {
            var token = UserService.GetTokenFromBrowser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}