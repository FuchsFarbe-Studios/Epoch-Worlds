// EpochWorlds
// EpochAuthProvider.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using System.Security.Claims;
using EpochApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;

namespace EpochApp.Client.Services
{
    public class EpochAuthProvider : AuthenticationStateProvider, IDisposable
    {
        private readonly EpochUserService _userService;
        public User CurrentUser { get; private set; } = new User();

        public EpochAuthProvider(EpochUserService userService)
        {
            _userService = userService;
            AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        }

        /// <inheritdoc />
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();
            var user = _userService.FetchUserFromBrowser();

            if (user is not null)
            {
                var authenticatedUser = await _userService.SendAuthenticateRequestAsync(user.UserName, user.Password);

                if (authenticatedUser is not null)
                {
                    principal = authenticatedUser.ToClaimsPrincipal();
                    CurrentUser = authenticatedUser;
                }
            }

            return new(principal);
        }

        private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
        {
            var authState = await task;
            if (authState is not null)
            {
                CurrentUser = User.FromClaimsPrincipal(authState.User);
            }
        }

        public async Task LoginAsync(string username, string password)
        {
            var principal = new ClaimsPrincipal();
            var user = await _userService.SendAuthenticateRequestAsync(username, password);

            if (user is not null)
            {
                principal = user.ToClaimsPrincipal();
            }

            CurrentUser = user;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void Logout()
        {
            _userService.ClearBrowserUserData();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
        }

        /// <inheritdoc />
        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
    }
}