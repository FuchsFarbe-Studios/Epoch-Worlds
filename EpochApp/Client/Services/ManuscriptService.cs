// EpochWorlds
// ManuscriptService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
using EpochApp.Shared;
using MudBlazor;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    /// <inheritdoc />
    public class ManuscriptService : IManuscriptService
    {
        private readonly HttpClient _client;
        private readonly ILogger<IManuscriptService> _logger;
        private readonly ISnackbar _snackbar;

        /// <summary>
        ///   Constructor for the ManuscriptService.
        /// </summary>
        /// <param name="client"> The HttpClient. </param>
        /// <param name="logger"> The logger. </param>
        /// <param name="snackbar"> The snackbar. </param>
        public ManuscriptService(HttpClient client, ILogger<ManuscriptService> logger, ISnackbar snackbar)
        {
            _client = client;
            _logger = logger;
            _snackbar = snackbar;
        }

        /// <inheritdoc />
        public async Task<List<ManuscriptDTO>> GetUserManuscripts(Guid userId)
        {
            var userManuscripts = await _client.GetFromJsonAsync<List<ManuscriptDTO>>($"api/v1/Articles/Manuscripts?userId={userId}");
            return await Task.FromResult(userManuscripts);
        }
    }
}