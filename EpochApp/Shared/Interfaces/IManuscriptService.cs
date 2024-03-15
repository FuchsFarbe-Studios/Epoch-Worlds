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

        /// <summary>
        ///   Get a manuscript by its unique identifier.
        /// </summary>
        /// <param name="manuscriptId"> The manuscript's unique identifier. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="ManuscriptDTO" />. </returns>
        Task<ManuscriptDTO> GetManuscriptAsync(long manuscriptId);

        /// <summary>
        ///  Create a new manuscript.
        /// </summary>
        /// <param name="manuscript"> The manuscript to create. </param>
        /// <returns> <see cref="Task{TResult}" /> of <see cref="ManuscriptDTO" />. </returns>
        Task<ManuscriptDTO> CreateManuscriptAsync(ManuscriptDTO manuscript);
    }
}