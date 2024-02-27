// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using EpochApp.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace EpochApp.Client.Services
{
    /// <summary>
    ///     Service for handling user authentication.
    /// </summary>
    public class EpochUserService
    {
        private readonly ClientAuthData _authData;
        private readonly HttpClient _client;

        /// <summary>
        ///     Constructor for the <see cref="EpochUserService" />.
        /// </summary>
        /// <param name="client"> Http client. </param>
        /// <param name="authData">
        ///     Client side authentication data.
        /// </param>
        public EpochUserService(HttpClient client, ClientAuthData authData)
        {
            _client = client;
            _authData = authData;
        }

        public async Task<UserData> SendRefreshTokenRequestAsync()
        {
            var response = await _client.PostAsync("api/v1/EpochUsers/Refresh-Token", null);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                var claimPrincipal = CreateClaimsPrincipalFromToken(token);
                var user = UserData.FromClaimsPrincipal(claimPrincipal);
                PersistUserToBrowser(token);
                return user;
            }
            return null;
        }

        /// <summary>
        ///     Sends a request to the server to authenticate the user.
        /// </summary>
        /// <param name="username"> Users username. </param>
        /// <param name="password"> Users password. </param>
        /// <returns>
        ///     <see cref="Task{TResult}" /> where TResult is <see cref="UserData" />.
        /// </returns>
        public async Task<UserData> SendAuthenticateRequestAsync(string username, string password)
        {
            // Don't send a request if the username or password is empty.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            var response = await _client.PostAsJsonAsync("api/v1/EpochUsers/Authentication", new LoginDTO { UserName = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                var claimPrincipal = CreateClaimsPrincipalFromToken(token);
                var user = UserData.FromClaimsPrincipal(claimPrincipal);
                PersistUserToBrowser(token);

                return user;
            }

            return null;
        }

        /// <summary>
        ///     Gets the client side user data.
        /// </summary>
        /// <returns>
        ///     <see cref="UserData" />
        /// </returns>
        public UserData FetchUserFromBrowser()
        {
            var claimsPrincipal = CreateClaimsPrincipalFromToken(_authData.Token);
            var user = UserData.FromClaimsPrincipal(claimsPrincipal);
            return user;
        }

        private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "jwt");
            }

            return new ClaimsPrincipal(identity);
        }

        /// <summary>
        ///     Retrieves the active token from the browser.
        /// </summary>
        /// <returns>
        ///     <see cref="string" />
        /// </returns>
        public string GetTokenFromBrowser()
        {
            return _authData.Token;
        }

        private void PersistUserToBrowser(string token)
        {
            _authData.Token = token;
        }

        /// <summary>
        ///     Clears the client side user data, effectively logging out the user.
        /// </summary>
        public void ClearBrowserUserData()
        {
            _authData.Token = "";
        }
    }
}