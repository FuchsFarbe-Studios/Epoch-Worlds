// EpochWorlds
// EpochAuthProvider.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EpochApp.Client.Services
{
    /// <summary>
    ///     The authentication provider for the application.
    /// </summary>
    public class EpochAuthProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly ILogger<EpochAuthProvider> _logger;
        private readonly EpochUserService _userService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EpochAuthProvider" /> class.
        /// </summary>
        /// <param name="userService"> The user service. </param>
        /// <param name="logger"> The logger. </param>
        public EpochAuthProvider(EpochUserService userService, ILogger<EpochAuthProvider> logger)
        {
            _userService = userService;
            _logger = logger;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        /// <summary>
        ///     Currently signed in User principal.
        /// </summary>
        public UserData CurrentUser { get; private set; } = new UserData();

        /// <inheritdoc />
        public void Dispose()
        {
            AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
        }

        /// <inheritdoc />
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = _userService.FetchUserFromBrowser();

            if (user is not null)
            {
                var authenticatedUser = await _userService.SendAuthenticateRequestAsync(user.UserName, user.Hash);
                if (authenticatedUser is not null)
                {
                    principal = authenticatedUser.ToClaimsPrincipal();
                    CurrentUser = authenticatedUser;
                }
            }

            return new AuthenticationState(principal);
        }

        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authState = await task;
            if (authState is not null)
                CurrentUser = UserData.FromClaimsPrincipal(authState.User);
        }

        /// <summary>
        ///     Logs in the user and updates the authentication state.
        /// </summary>
        /// <param name="username">
        ///     The username of the user.
        /// </param>
        /// <param name="password">
        ///     The password of the user.
        /// </param>
        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await _userService.SendAuthenticateRequestAsync(username, password);

            if (user is null)
            {
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
                CurrentUser = null;
                return;
            }
            principal = user.ToClaimsPrincipal();
            CurrentUser = user;
            _logger.LogInformation($"User: {CurrentUser.UserName} logged in.");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        /// <summary>
        ///     Logs out the user and updates the authentication state.
        /// </summary>
        public void Logout()
        {
            _logger.LogInformation($"User: {CurrentUser.UserName} logged out.");
            _userService.ClearBrowserUserData();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}