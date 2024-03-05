// EpochWorlds
// IManuscriptService.cs
// FuchsFarbe Studios 2024
// matsu
// Modified: 4-3-2024
namespace EpochApp.Shared
{
    /// <summary>
    ///    Service for managing manuscripts.
    /// </summary>
    public interface IManuscriptService
    {
        /// <summary>
        ///    Get all manuscripts for a user.
        /// </summary>
        /// <param name="userId"> The user's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="List{T}" /> of <see cref="ManuscriptDTO" />. </returns>
        Task<List<ManuscriptDTO>> GetUserManuscripts(Guid userId);
    }
}