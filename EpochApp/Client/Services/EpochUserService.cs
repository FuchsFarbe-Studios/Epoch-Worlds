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
    public class EpochUserService
    {
        private readonly ClientAuthData _authData;
        private readonly HttpClient _client;

        public EpochUserService(HttpClient client, ClientAuthData authData)
        {
            _client = client;
            _authData = authData;
        }

        public async Task<UserData> SendAuthenticateRequestAsync(string username, string password)
        {
            // Don't send a request if the username or password is empty.
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var response = await _client.PostAsJsonAsync("api/v1/EpochUsers/Authentication"
                                                         , new LoginDTO { UserName = username, Password = password });

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

        private void PersistUserToBrowser(string token)
        {
            _authData.Token = token;
        }

        public void ClearBrowserUserData()
        {
            _authData.Token = "";
        }
    }
}