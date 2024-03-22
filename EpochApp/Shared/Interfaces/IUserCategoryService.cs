// EpochWorlds
// IUserCategoryService.cs
//  2024
// Oliver Conover
// Modified: 21-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///   Service for managing user categories.
    /// </summary>
    public interface IUserCategoryService
    {
        /// <summary>
        ///  Get all user categories for a user.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="List{T}" /> of <see cref="UserCategoryDTO" />. </returns>
        Task<List<UserCategoryDTO>> GetUserCategoriesAsync(Guid userId);

        /// <summary>
        ///  Create a new user category.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="category"> The user category to create. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="UserCategoryDTO" />. </returns>
        Task<UserCategoryDTO> CreateUserCategoryAsync(Guid userId, UserCategoryDTO category);

        /// <summary>
        /// Update a user category.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="categoryId"> The category's unique identifier. </param>
        /// <param name="category"> The user category to update. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="UserCategoryDTO" />. </returns>
        Task<UserCategoryDTO> UpdateUserCategoryAsync(Guid userId, int categoryId, UserCategoryDTO category);

        /// <summary>
        /// Delete a user category.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="categoryId"> The category's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="bool" />. </returns>
        Task<bool> DeleteUserCategoryAsync(Guid userId, int categoryId);

        /// <summary>
        /// Get a user category.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <param name="categoryId"> The category's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="UserCategoryDTO" />. </returns>
        Task<UserCategoryDTO> GetUserCategoryAsync(Guid userId, int categoryId);
    }
}