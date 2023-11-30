// EpochWorlds
// EpochUserService.cs
// FuchsFarbe Studios 2023
// Oliver MacDougall
// Modified: 29-11-2023

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

using EpochApp.Shared;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

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

        public async Task<UserData> SendAuthenticateRequestAsync(string username, string password)
        {
            var response = await _client.PostAsJsonAsync($"/api/v1/EpochUsers/Authenticate",new LoginDTO(){UserName = username, Password = password});

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
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
                identity = new(jwtSecurityToken.Claims, "jwt");
            }

            return new(identity);
        }

        private void PersistUserToBrowser(string token) => _authData.Token = token;

        public void ClearBrowserUserData() => _authData.Token = "";


    }
}