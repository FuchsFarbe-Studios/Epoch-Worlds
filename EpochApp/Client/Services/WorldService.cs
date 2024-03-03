// EpochWorlds
// WorldService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 29-2-2024
using EpochApp.Shared;
using EpochApp.Shared.Users;
using EpochApp.Shared.Worlds;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    /// <summary>
    ///     Service for handling and modifying World Data.
    /// </summary>
    public class WorldService : IWorldService
    {
        private readonly HttpClient _client;
        private readonly IWebAssemblyHostEnvironment _host;
        private readonly ILogger<IWorldService> _logger;

        /// <summary>
        ///     Constructor for WorldService.
        /// </summary>
        /// <param name="logger"> The logger. </param>
        /// <param name="host">
        ///     The web assembly host environment.
        /// </param>
        /// <param name="client"> The http client. </param>
        public WorldService(ILogger<WorldService> logger, IWebAssemblyHostEnvironment host, HttpClient client)
        {
            _host = host;
            _client = client;
            _logger = logger;
            // _client = new HttpClient { BaseAddress = new Uri($"{_host.BaseAddress}") };
            _logger.LogInformation($"WorldService created, base address: {_client.BaseAddress}");
        }

        /// <inheritdoc />
        public async Task<List<UserWorldDTO>> GetWorldsAsync()
        {
            var worlds = await _client.GetFromJsonAsync<List<UserWorldDTO>>("api/v2/Worlds");
            return null;
        }

        /// <inheritdoc />
        public async Task<List<UserWorldDTO>> GetUserWorldsAsync(Guid userId)
        {
            var worlds = await _client.GetFromJsonAsync<List<UserWorldDTO>>($"api/v2/Worlds/UserWorlds?userId={userId}");
            worlds ??= new List<UserWorldDTO>();
            return await Task.FromResult(worlds);
        }

        /// <summary>
        /// Should never be called client side.
        /// </summary>
        /// <returns>null</returns>
        public Task<World> CreateRegistrationWorldAsync(RegistrationDTO registration, User user)
        {
            return null;
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> CreateWorldAsync(UserWorldDTO world)
        {
            var response = await _client.PostAsJsonAsync<UserWorldDTO>("api/v2/Worlds", world);
            if (response.IsSuccessStatusCode)
            {
                var newWorld = await response.Content.ReadFromJsonAsync<UserWorldDTO>();
                _logger.LogInformation($"Created world {newWorld.WorldId} - {newWorld.WorldName}");
                return await Task.FromResult(newWorld);
            }
            _logger.LogWarning("Failed to create world!");
            return null;
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> GetWorldAsync(Guid worldId)
        {
            var response = await _client.GetFromJsonAsync<UserWorldDTO>($"api/v2/Worlds/{worldId}");
            return await Task.FromResult(response);
        }

        /// <inheritdoc />
        public async Task<World> GetWorldViewAsync(Guid worldId)
        {
            var response = await _client.GetFromJsonAsync<World>($"api/v2/Worlds/View/{worldId}");
            return await Task.FromResult(response);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> UpdateWorldAsync(UserWorldDTO world)
        {
            var response = await _client.PutAsJsonAsync<UserWorldDTO>($"api/v2/Worlds/{world.WorldId}", world);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Updated world {WorldID}", world.WorldId);
            }
            else
            {
                _logger.LogWarning("World failed to update!");
                return null;
            }
            var updateWorld = await response.Content.ReadFromJsonAsync<UserWorldDTO>();
            return await Task.FromResult(updateWorld);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> UpdateActiveUserWorldsAsync(UserWorldDTO world)
        {
            var response = await _client.PutAsJsonAsync<UserWorldDTO>("api/v2/Worlds/ActiveWorld", world);
            response.EnsureSuccessStatusCode();
            var update = await response.Content.ReadFromJsonAsync<UserWorldDTO>();
            return await Task.FromResult(update);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> DeleteWorldAsync(Guid userId, Guid worldId)
        {
            var response = await _client.DeleteFromJsonAsync<UserWorldDTO>($"api/v2/Worlds?userId={userId}&worldId={worldId}");
            if (response == null)
            {
                _logger.LogWarning("World not found!");
                return null;
            }
            return await Task.FromResult(response);
        }

        /// <inheritdoc />
        public async Task<UserWorldDTO> GetActiveWorldAsync(Guid userId)
        {
            var activeWorld = await _client.GetFromJsonAsync<UserWorldDTO>($"api/v2/Worlds/ActiveWorld?ownerId={userId}");
            return await Task.FromResult(activeWorld);
        }
    }
}