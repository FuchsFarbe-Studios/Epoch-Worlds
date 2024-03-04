// EpochWorlds
// ProfileService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    #pragma warning disable CS1591
    /// <inheritdoc />
    public class ProfileService : IProfileSerivce
    {
        private readonly HttpClient _client;
        private readonly ILogger<IProfileSerivce> _logger;
        private readonly ISnackbar _snackbar;

        public ProfileService(HttpClient client, ILogger<ProfileService> logger, ISnackbar snackbar)
        {
            _client = client;
            _logger = logger;
            _snackbar = snackbar;
        }

        /// <inheritdoc />
        public async Task<ProfileDTO> GetProfileByUsername(string userName)
        {
            var profile = await _client.GetFromJsonAsync<ProfileDTO>($"api/v1/Profiles/Profile?userName={userName}");
            return await Task.FromResult(profile);
        }

        /// <inheritdoc />
        public async Task<ProfileDTO> GetProfileByUserIdAsync(Guid userId)
        {
            var profile = await _client.GetFromJsonAsync<ProfileDTO>($"api/v1/Profiles/Profile/{userId}");
            return await Task.FromResult(profile);
        }

        /// <inheritdoc />
        public async Task<ProfileDTO> UpdateProfile(Guid userId, ProfileDTO profile)
        {
            var response = await _client.PutAsJsonAsync($"api/v1/Profiles/Profile/{userId}", profile);
            if (response.IsSuccessStatusCode)
            {
                var updatedProfile = await response.Content.ReadFromJsonAsync<ProfileDTO>();
                return await Task.FromResult(updatedProfile);
            }
            return null;
        }
    }
}