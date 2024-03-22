// EpochWorlds
// UserCategoryService.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
using EpochApp.Shared;
using System.Net.Http.Json;

namespace EpochApp.Client.Services
{
    #pragma warning disable CS1591
    public class UserCategoryService : IUserCategoryService
    {
        private readonly HttpClient _client;
        private readonly ILogger<IUserCategoryService> _logger;

        public UserCategoryService(HttpClient client, ILogger<UserCategoryService> logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<List<UserCategoryDTO>> GetUserCategoriesAsync(Guid userId)
        {
            var categories = await _client.GetFromJsonAsync<List<UserCategoryDTO>>($"api/v1/Articles/UserCategories/{userId}");
            return await Task.FromResult(categories);
        }

        /// <inheritdoc />
        public async Task<UserCategoryDTO> CreateUserCategoryAsync(Guid userId, UserCategoryDTO category)
        {
            var response = await _client.PostAsJsonAsync($"api/v1/Articles/UserCategories/{userId}", category);
            if (response.IsSuccessStatusCode)
            {
                var newCategory = await response.Content.ReadFromJsonAsync<UserCategoryDTO>();
                return await Task.FromResult(newCategory);
            }
            return null;
        }

        /// <inheritdoc />
        public async Task<UserCategoryDTO> UpdateUserCategoryAsync(Guid userId, int categoryId, UserCategoryDTO category)
        {
            var response = await _client.PutAsJsonAsync($"api/v1/Articles/UserCategories/{userId}/{categoryId}", category);
            if (response.IsSuccessStatusCode)
            {
                var updatedCategory = await response.Content.ReadFromJsonAsync<UserCategoryDTO>();
                return await Task.FromResult(updatedCategory);
            }
            return null;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteUserCategoryAsync(Guid userId, int categoryId)
        {
            var response = await _client.DeleteAsync($"api/v1/Articles/UserCategories/{userId}/{categoryId}");
            return await Task.FromResult(response.IsSuccessStatusCode);
        }

        /// <inheritdoc />
        public async Task<UserCategoryDTO> GetUserCategoryAsync(Guid userId, int categoryId)
        {
            var category = await _client.GetFromJsonAsync<UserCategoryDTO>($"api/v1/Articles/UserCategories/Category/{userId}/{categoryId}");
            return await Task.FromResult(category);
        }
    }
}