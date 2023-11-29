// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EpochApp.Shared;

namespace EpochApp.Client.Services
{
    public class EpochUserService
    {
        private readonly HttpClient _client;
        private readonly ClientAuthData _authData;

        public EpochUserService(HttpClient client, ClientAuthData authData)
        {
            _client = client;
            _authData = authData;
        }

        public async Task<User> SendAuthenticateRequestAsync(string username, string password)
        {
            var response = await _client.GetAsync($"/example-data/{username}.json");

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                var claimPrincipal = CreateClaimsPrincipalFromToken(token);
                var user = User.FromClaimsPrincipal(claimPrincipal);
                PersistUserToBrowser(token);

                return user;
            }

            return null;
        }

        public User FetchUserFromBrowser()
        {
            var claimsPrincipal = CreateClaimsPrincipalFromToken(_authData.Token);
            var user = User.FromClaimsPrincipal(claimsPrincipal);

            return user;
        }

        private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                identity = new(jwtSecurityToken.Claims, "Blazor School");
            }

            return new(identity);
        }

        private void PersistUserToBrowser(string token) => _authData.Token = token;

        public void ClearBrowserUserData() => _authData.Token = "";
    }
}